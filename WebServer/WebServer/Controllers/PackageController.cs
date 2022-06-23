using PhotostudioDB;
using RestPanda.Requests;
using RestPanda.Requests.Attributes;
using WebServer.Models;

namespace WebServer.Controllers;

[RequestHandlerPath("/package")]
public class PackageController : RequestHandler
{
    [Get("/get")]
    public void GetPackages()
    {
        using var db = new ApplicationContext();
        var packages = db.ServicePackages.OrderBy(s => s.Title);
        Send(new AnswerModel(true, new {packages}, null));
    }
}