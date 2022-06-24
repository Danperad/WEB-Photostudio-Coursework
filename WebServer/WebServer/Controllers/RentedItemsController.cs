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
        var type = GetParams<int>("type");
        if (startDate == 0 && duration == 0)
        {
            Send(new AnswerModel(false, null, 101));
            return;
        }

        var date = new DateTime(1970, 1, 1, 3, 0, 0, 0).AddMilliseconds(startDate);
        using var db = new ApplicationContext();
        var halls = GetFreeItems(type,date, duration, db);
        Send(new AnswerModel(true, new {items = halls}, null));
    }

    private static IEnumerable<RentedItemModel> GetFreeItems(int type, DateTime startDate, int duration, ApplicationContext db)
    {
        var item = db.RentedItems.ToList();
        item = type switch
        {
            6 => item.Where(i => !i.IsСlothes).ToList(),
            5 => item.Where(i => i.IsСlothes && !i.IsKids).ToList(),
            _ => item.Where(i => i.IsKids).ToList()
        };
        var services = db.ApplicationServices.Where(a =>
            a.Service.Type == 4 && a.StartDateTime.HasValue && a.StartDateTime.Value.Date == startDate.Date);

        services = services.Where(a => item.Any(i => i.Id == a.Id));
        services = services.Where(a =>
            !Program.GetTimed(60, startDate, duration, a.StartDateTime!.Value, a.Duration!.Value));
        var newItems = RentedItemModel.GetModels(item).ToList();
        foreach (var model in newItems)
        {
            var count = services.Where(s => s.RentedItemId == model.Id).Sum(s => s.Number!.Value);
            if (count > model.Number) model.Number = 0;
            else model.Number -= (uint) count;
        }

        return newItems.Where(i => i.Number != 0);
    }
}