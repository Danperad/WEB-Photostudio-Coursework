using PhotostudioDB;
using PhotostudioDB.Models;
using RestPanda.Requests;
using RestPanda.Requests.Attributes;
using WebServer.Models;

namespace WebServer.Controllers;

[RequestHandlerPath("/employee")]
public class EmployeeController : RequestHandler
{
    [Get("/getfree")]
    public void GetFree()
    {
        var service = GetParams<int>("service");
        var startDate = GetParams<long>("start");
        var duration = GetParams<int>("duration");
        if (startDate == 0 && duration == 0)
        {
            Send(new AnswerModel(false, null, 101));
            return;
        }
        
        var date = new DateTime(1970, 1, 1, 3, 0, 0, 0).AddMilliseconds(startDate);
        using var db = new ApplicationContext();
        var employees = GetFreeEmployees(service, date, duration, db).ToList();
        Send(new AnswerModel(true, new {employees = EmployeeModel.GetModels(employees)}, null));
    }

    private IEnumerable<Employee> GetFreeEmployees(int service, DateTime startDate, int duration, ApplicationContext db)
    {
        var employees = service switch
        {
            2 => db.Employees.Where(e => e.RoleId == 4).ToList(),
            12 => db.Employees.Where(e => e.RoleId == 4).ToList(),
            13 => db.Employees.Where(e => e.RoleId == 6).ToList(),
            _ => db.Employees.Where(e => e.RoleId == 2).ToList()
        };
        var serviceType = service == 13 ? 5 : 3;
        var services = db.ApplicationServices.Where(a =>
            a.Service.Type == serviceType && a.StartDateTime.HasValue && a.StartDateTime.Value.Date == startDate.Date).ToList();

        services = services.Where(a => employees.Any(e => e.Id == a.EmployeeId)).ToList();
        services = services.Where(a =>
            !Program.GetTimed(90, startDate, duration, a.StartDateTime!.Value, a.Duration!.Value)).ToList();
        return employees.Where(e => services.All(a => a.EmployeeId != e.Id)).ToList();
    }
}