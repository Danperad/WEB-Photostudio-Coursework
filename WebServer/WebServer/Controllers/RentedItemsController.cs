using PhotostudioDB;
using RestPanda.Requests;
using RestPanda.Requests.Attributes;
using WebServer.Models;

namespace WebServer.Controllers;

[RequestHandlerPath("/renteditems")]
public class RentedItemsController : RequestHandler
{
    [Get("/get")]
    public void GetItems()
    {
        var type = GetParams<int>("type");
        if (type == 0)
        {
            Send(new AnswerModel(false, null, 101));
            return;
        }

        using var db = new ApplicationContext();
        var items = type switch
        {
            5 => db.RentedItems.Where(i => i.IsСlothes && !i.IsKids).OrderBy(i => i.Title),
            6 => db.RentedItems.Where(i => !i.IsСlothes).OrderBy(i => i.Title),
            _ => db.RentedItems.Where(i => i.IsKids).OrderBy(i => i.Title)
        };

        Send(new AnswerModel(true, new {items = RentedItemModel.GetModels(items)}, null));
    }

    [Get("/getfree")]
    public void GetFree()
    {
        var startDate = GetParams<long>("start");
        var duration = GetParams<int>("duration");
        var id = GetParams<int>("id");
        if (startDate == 0 && duration == 0)
        {
            Send(new AnswerModel(false, null, 101));
            return;
        }

        using var db = new ApplicationContext();
        var halls = GetFreeItems(id, new DateTime(startDate), duration, db);
        Send(new AnswerModel(true, new {count = halls}, null));
    }

    private static uint GetFreeItems(int id, DateTime startDate, int duration, ApplicationContext db)
    {
        var item = db.RentedItems.First(i => i.Id == id);
        var services = db.ApplicationServices.Where(a =>
            a.Service.Type == 4 && a.StartDateTime.HasValue && a.StartDateTime.Value.Date == startDate.Date);

        services = services.Where(a => a.RentedItemId == id);
        services = services.Where(a =>
            !Program.GetTimed(60, startDate, duration, a.StartDateTime!.Value, a.Duration!.Value));
        var sum = services.Sum(s => s.Number!.Value);
        if (sum >= item.Number) return 0;
        return (uint) (item.Number - sum);
    }
}