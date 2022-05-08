using PhotostudioDB.Models;
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
        Send(new AnswerModel(true, new {services = Service.GetServices()}, null, null));
    }
}