using Microsoft.EntityFrameworkCore;
using PhotostudioDB;
using PhotostudioDB.Models;
using RestPanda.Requests;
using RestPanda.Requests.Attributes;
using WebServer.Models;

namespace WebServer.Controllers;

[RequestHandlerPath("/hall")]
public class HallController : RequestHandler
{
    [Get("/get")]
    public void GetHalls()
    {
        using var db = new ApplicationContext();
        var halls = db.Halls.Include(h => h.Address).OrderBy(h => h.Title);
        Send(new AnswerModel(true, new {halls = HallModel.GetHallModels(halls)}, null));
    }

    [Get("/getfree")]
    [ResponseTimeout(1200000)]
    public void GetFree()
    {
        var startDate = GetParams<long>("start");
        var duration = GetParams<int>("duration");
        if (startDate == 0 && duration == 0)
        {
            Send(new AnswerModel(false, null, 101));
            return;
        }

        var date = new DateTime(1970, 1, 1, 3, 0, 0, 0).AddMilliseconds(startDate);
        using var db = new ApplicationContext();
        var halls = GetFreeHalls(date, duration, db).OrderBy(h => h.Title).ToList();
        Send(new AnswerModel(true, new {halls = HallModel.GetHallModels(halls)}, null));
    }

    private static IEnumerable<Hall> GetFreeHalls(DateTime startDate, int duration, ApplicationContext db)
    {
        var halls = db.Halls.ToList();
        var services = db.ApplicationServices.Where(a =>
            a.Service.Type == 2 && a.StartDateTime.HasValue && a.StartDateTime.Value.Date == startDate.Date).ToList();

        services = services.Where(a => halls.Any(h => h.Id == a.HallId!.Value)).ToList();
        services = services.Where(a =>
            !Program.GetTimed(90, startDate, duration, a.StartDateTime!.Value, a.Duration!.Value)).ToList();
        return halls.Where(h => services.All(a => a.HallId != h.Id)).ToList();
    }
}