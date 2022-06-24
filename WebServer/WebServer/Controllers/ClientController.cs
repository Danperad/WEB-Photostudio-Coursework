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
    [ResponseTimeout(int.MaxValue - 1)]
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
        if (cart is null || (cart.ServiceModels.Count == 0 && cart.ServicePackage is null))
        {
            Send(new AnswerModel(false, null, 400));
            return;
        }

        List<ApplicationService> services;
        using var db = new ApplicationContext();
        try
        {
            services = NewServiceModel.GetServices(cart.ServiceModels, db).ToList();
        }
        catch
        {
            Send(new AnswerModel(false, null, 400));
            return;
        }

        ServicePackage? package = null;
        if (cart.ServicePackage is not null)
        {
            package = db.ServicePackages.Include(p => p.Services).FirstOrDefault(s => s.Id == cart.ServicePackage.Id);
            if (package is not null)
            {
                var startTime = new DateTime(1970, 1, 1, 3, 0, 0, 0).AddMilliseconds(cart.ServicePackage.StartTime);
                var lst = new List<ApplicationService>();
                var status = db.Statuses.First(st => st.Id == 7);
                foreach (var service in package.Services.OrderBy(s => s.Type))
                {
                    switch (service.Type)
                    {
                        case 1:
                            var employee = service.Id switch
                            {
                                3 => db.Employees.Single(e => e.RoleId == 3),
                                4 => db.Employees.Single(e => e.RoleId == 5),
                                _ => db.Employees.Single(e => e.RoleId == 7)
                            };
                            lst.Add(new ApplicationService(service, employee, status));
                            break;
                        case 2:
                            if (package.Hall is null) db.Halls.Where(h => h.Id == package.HallId).Load();
                            lst.Add(new ApplicationService(service, db.Employees.Single(e => e.RoleId == 7), startTime,
                                package.Duration, package.Hall!, status));
                            break;
                        case 3:
                            if (package.HallId.HasValue)
                            {
                                if (package.Hall!.Address is null)
                                    db.Addresses.Where(a => a.Id == package.Hall.AddressId).Load();
                                lst.Add(new ApplicationService(service,
                                    db.Employees.First(e => e.Id == package.EmployeeId), startTime, package.Duration,
                                    package.Hall!.Address!, status));
                            }
                            else
                            {
                                if (package.Address is null) db.Addresses.Where(a => a.Id == package.AddressId).Load();
                                lst.Add(new ApplicationService(service,
                                    db.Employees.First(e => e.Id == package.EmployeeId), startTime, package.Duration,
                                    package.Address!, status));
                            }

                            break;
                        case 5:
                            if (package.HallId.HasValue)
                            {
                                if (package.Hall!.Address is null)
                                    db.Addresses.Where(a => a.Id == package.Hall.AddressId).Load();
                                lst.Add(new ApplicationService(service,
                                    db.Employees.First(e => e.Id == package.EmployeeId), startTime, package.Duration,
                                    package.Hall!.Address!, true, status));
                            }
                            else
                            {
                                if (package.Address is null) db.Addresses.Where(a => a.Id == package.AddressId).Load();
                                lst.Add(new ApplicationService(service,
                                    db.Employees.First(e => e.Id == package.EmployeeId), startTime, package.Duration,
                                    package.Address!, true, status));
                            }

                            break;
                    }
                }

                services.AddRange(lst);
            }
        }

        var cl = db.Clients.Include(c => c.Profile).First(c => c.Id == client.Id);
        var order = new Order(cl, DateTime.Now, services, db.Statuses.First(st => st.Id == 3), package);
        var lastDay = DateOnly.FromDateTime(DateTime.Today).AddDays(4);
        foreach (var service in services.Where(service => service.StartDateTime.HasValue))
        {
            lastDay = DateOnly.FromDateTime(service.StartDateTime!.Value).AddDays(4);
        }

        var contract = new Contract(order, cl, db.Employees.Single(e => e.RoleId == 7),
            DateOnly.FromDateTime(DateTime.Today).AddDays(1), lastDay);

        if (!contract.Check() || !order.Check())
        {
            Send(new AnswerModel(false, null, 400));
            return;
        }

        db.Orders.Add(order);
        db.Contracts.Add(contract);
        db.Entry(cl).State = EntityState.Unchanged;
        if (db.SaveChanges() < 0)
        {
            Send(new AnswerModel(false, null, 400));
            return;
        }

        Send(new AnswerModel(true, null, null));
        var from = new MailAddress("kokorin-02@mail.ru", "Sunrise");
        var to = new MailAddress(client.EMail, client.FullName);
        var msg = new MailMessage(from, to);
        msg.Subject = $"Чек от {DateTime.Now:g}";
        msg.IsBodyHtml = true;
        msg.Body = "<h1>Покупка прошла успешно</h1>";

        using var smtp = new SmtpClient("smtp.mail.ru", 587);
        smtp.Credentials = new NetworkCredential("kokorin-02@mail.ru", "nNq2dL27aj0diPFzicSI");
        smtp.EnableSsl = true;
        smtp.Send(msg);
    }
}