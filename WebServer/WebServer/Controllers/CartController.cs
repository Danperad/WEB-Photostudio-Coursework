using PhotostudioDB;
using PhotostudioDB.Models;
using RestPanda.Requests;
using RestPanda.Requests.Attributes;
using WebServer.Models;

namespace WebServer.Controllers;

[RequestHandlerPath("/cart")]
public class CartController : RequestHandler
{
    [Post("/checkservice")]
    public void CheckService()
    {
        var id = GetParams<int>("id");
        var body = Bind<NewOrderModel>();
        if (body is null)
        {
            Send(new AnswerModel(false, null, 401));
            return;
        }

        using var db = new ApplicationContext();
        var service = db.Services.FirstOrDefault(s => s.Id == id);
        if (service is null)
        {
            Send(new AnswerModel(false, null, 401));
            return;
        }
        
        Send(new AnswerModel(true, true, null));
    }
}