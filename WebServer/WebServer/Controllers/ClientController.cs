using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using PhotostudioDB;
using PhotostudioDB.Models;
using RestPanda.Requests;
using RestPanda.Requests.Attributes;
using WebServer.Models;

namespace WebServer.Controllers;

[RequestHandlerPath("/client")]
public class ClientController : RequestHandler
{
    [Post("/update")]
    public void UpdateClient()
    {
        if (!Headers.TryGetValue("Authorization", out var token) || !TokenWorker.CheckToken(token))
        {
            Send(new AnswerModel(false, null, 400));
            return;
        }

        var client = TokenWorker.GetClientByToken(token);
        if (client is null)
        {
            Send(new AnswerModel(false, null, 400));
            return;
        }

        using var db = new ApplicationContext();
        var body = Bind<ClientModel>();
        if (body is null)
        {
            Send(new AnswerModel(false, null, 400));
            return;
        }

        if (!string.IsNullOrWhiteSpace(body.Avatar) && client.Avatar != body.Avatar) client.Avatar = body.Avatar;
        if (!string.IsNullOrWhiteSpace(body.FirstName) && client.FirstName != body.FirstName)
            client.FirstName = body.FirstName;
        if (!string.IsNullOrWhiteSpace(body.LastName) && client.LastName != body.LastName)
            client.LastName = body.LastName;
        if (!string.IsNullOrWhiteSpace(body.MiddleName) && client.MiddleName != body.MiddleName)
            client.MiddleName = body.MiddleName;
        if (!string.IsNullOrWhiteSpace(body.EMail) && client.EMail != body.EMail &&
            !db.Clients.Any(c => c.EMail == body.EMail)) client.EMail = body.EMail;
        if (!string.IsNullOrWhiteSpace(body.Phone) && client.Phone != body.Phone &&
            !db.Clients.Any(c => c.Phone == body.Phone)) client.Phone = body.Phone;
        if (client.Profile is null) db.Profiles.Where(p => p.Id == client.ProfileId).Load();
        if (!string.IsNullOrWhiteSpace(body.Login) && client.Profile!.Login != body.Login &&
            !db.Profiles.Any(p => p.Login == body.Login))
            client.Profile.Login = body.Login;

        if (!client.Check())
        {
            Send(new AnswerModel(false, null, 400));
            return;
        }

        db.Update(client);
        db.Update(client.Profile!);

        if (db.SaveChanges() < 0)
        {
            Send(new AnswerModel(false, null, 400));
            return;
        }

        Send(new AnswerModel(true, new
        {
            user = ClientModel.GetClientModel(client)
        }, null));
    }

    [Get("/get")]
    public void GetClient()
    {
        if (!Headers.TryGetValue("Authorization", out var token) || !TokenWorker.CheckToken(token))
        {
            Send(new AnswerModel(false, null, 406));
            return;
        }

        var client = TokenWorker.GetClientByToken(token);
        if (client is null)
        {
            Send(new AnswerModel(false, null, 400));
            return;
        }

        Send(new AnswerModel(true, new
        {
            user = ClientModel.GetClientModel(client)
        }, null));
    }

    [Post("/buy")]
    public void AddOrder()
    {
        if (!Headers.TryGetValue("Authorization", out var token) || !TokenWorker.CheckToken(token))
        {
            Send(new AnswerModel(false, null, 400));
            return;
        }

        var client = TokenWorker.GetClientByToken(token);
        if (client is null)
        {
            Send(new AnswerModel(false, null, 400));
            return;
        }

        var cart = Bind<NewOrderModel>();
        if (cart is null || cart.ServiceModels.Count == 0 || cart.ServicePackage is null)
        {
            Send(new AnswerModel(false, null, 400));
            return;
        }

        var services = NewServiceModel.GetServices(cart.ServiceModels).ToList();
        var order = new Order(client, DateTime.Now, services);
        var lastDay = DateOnly.FromDateTime(DateTime.Today).AddDays(4);
        foreach (var service in services.Where(service => service.StartDateTime.HasValue))
        {
            lastDay = DateOnly.FromDateTime(service.StartDateTime!.Value).AddDays(4);
        }

        using var db = new ApplicationContext();
        var contract = new Contract(order, client, db.Employees.Single(e => e.RoleId == 7),
            DateOnly.FromDateTime(DateTime.Today).AddDays(1), lastDay);

        if (!contract.Check() || !order.Check())
        {
            Send(new AnswerModel(false, null, 400));
            return;
        }

        db.Orders.Add(order);
        db.Contracts.Add(contract);
        if (db.SaveChanges() < 0)
        {
            Send(new AnswerModel(false, null, 400));
            return;
        }
        
        Send(new AnswerModel(true, null, null));
        var from = new MailAddress("kockorinegor@yandex.ru", "Sunrise");
        var to = new MailAddress(client.EMail, client.FullName);
        var msg = new MailMessage(from, to);
        msg.Subject = $"Чек от {DateTime.Now:g}";
        msg.Body = "";
        
        using var smtp = new SmtpClient("smtp.yandex.ru", 465);
        smtp.Credentials = new NetworkCredential("kockorinegor@yandex.ru", "iknhcnhyozlvysif");
        smtp.EnableSsl = true;
        smtp.Send(msg);
    }
}