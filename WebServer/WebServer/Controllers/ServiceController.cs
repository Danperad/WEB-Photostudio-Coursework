using System.Web;
using PhotostudioDB;
using RestPanda.Requests;
using RestPanda.Requests.Attributes;
using WebServer.Models;

namespace WebServer.Controllers;

[RequestHandlerPath("/services")]
public class ServiceController : RequestHandler
{
    [Get("/get")]
    public void GetServices()
    {
        var count = GetParams<int>("count");
        var startWith = GetParams<int>("start");
        var order = GetParams<int>("order");
        var type = GetParams<int>("type");
        var search = GetParams<string>("search");
        using var db = new ApplicationContext();
        var services = type != 0 ? db.Services.Where(s => s.Type == type).ToList() : db.Services.ToList();
        services = order switch
        {
            2 => services.OrderBy(o => o.Cost).ThenBy(o => o.Title).ToList(),
            3 => services.OrderByDescending(o => o.Rating).ThenBy(o => o.Title).ToList(),
            _ => services.OrderBy(o => o.Title).ToList()
        };
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = HttpUtility.UrlDecode(search);
            services = services.Where(s => s.Title.ToLower().Contains(search.ToLower())).ToList();
        }

        var res = services;
        if (startWith != 0 && count != 0)
            res = services.Take(new Range(startWith - 1, startWith - 1 + count)).ToList();
        var has = res.LastOrDefault() != null && services.LastOrDefault() != null &&
                    res.Last().Id != services.Last().Id;
        Send(new AnswerModel(true, new {services = ServiceModel.ConvertList(res), hasMore = has }, null));
    }
}