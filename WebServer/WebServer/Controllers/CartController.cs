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

        var res = CheckAvailable(body, service, db);

        Send(new AnswerModel(true, res, null));
    }

    private bool CheckAvailable(NewOrderModel orderModel, Service service, ApplicationContext db)
    {
        var services = orderModel.ServiceModels.Select(s => db.Services.First(a => a.Id == s.Service.Id)).ToList();
        /*if (service.Type == 3 && services.Any(s => s.Type == 3)) return false;
        if (service.Type == 1 && services.All(s => s.Type != 3)) return false;
        if (service.Type == 2 && services.Any(s => s.Type == 2)) return false;
        if (service.Id == 4 && services.Any(a => a.Id is 2 or 12) && services.All(a => a.Id != service.Id))
            return true;
        if (service.Id is 3 or 8 && services.Any(a => a.Type == 3 && a.Id is not (2 or 12)) &&
            services.All(a => a.Id != service.Id))
            return true;
        if (service.Type == 5 && services.Any(a => a.Type == 3) && services.All(a => a.Id != service.Id)) return true;*/
        /*if (service.Type == 3 && services.Any(s => s.Type == 3)) return false;
        if (service.Type == 1 && services.All(s => s.Type != 3)) return false;
        return services.All(s => s.Id != service.Id);*/
        return true;
    }
}