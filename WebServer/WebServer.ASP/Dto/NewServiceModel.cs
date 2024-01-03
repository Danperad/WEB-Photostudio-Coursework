using Microsoft.EntityFrameworkCore;
using PhotostudioDB;
using PhotostudioDB.Models;

namespace WebServer.ASP.Dto;

public class NewServiceModel
{
    public int ServiceId { get; set; }
    public DateTime? StartDateTime { get; set; }
    public int? Duration { get; set; }
    public int? HallId { get; set; }
    public int? EmployeeId { get; set; }
    public string? Address { get; set; }
    public int? RentedItemId { get; set; }
    public int? Number { get; set; }
    public bool? IsFullTime { get; set; }

    public static ApplicationService GetService(NewServiceModel serviceModel, ApplicationContext db)
    {
        var service = db.Services.FirstOrDefault(s => s.Id == serviceModel.ServiceId);
        if (service is null) throw new Exception();
        var status = db.Statuses.First(st => st.Id == 7);
        switch ((int) service.Type)
        {
            case 1:
                var employee = service.Id switch
                {
                    3 => db.Employees.Single(e => e.RoleId == 3),
                    4 => db.Employees.Single(e => e.RoleId == 5),
                    _ => db.Employees.Single(e => e.RoleId == 7)
                };

                return new ApplicationService(service, employee, status);
            case 2:
                if (!serviceModel.HallId.HasValue) throw new Exception();
                var hall = db.Halls.FirstOrDefault(h => h.Id == serviceModel.HallId.Value);
                if (hall is null) throw new Exception();
                if (!serviceModel.StartDateTime.HasValue) throw new Exception();
                if (!serviceModel.Duration.HasValue) throw new Exception();
                return new ApplicationService(service, db.Employees.Single(e => e.RoleId == 7),
                    serviceModel.StartDateTime.Value,
                    serviceModel.Duration.Value, hall, status);
            case 3:
                if (!serviceModel.StartDateTime.HasValue) throw new Exception();
                if (!serviceModel.Duration.HasValue) throw new Exception();
                if (!serviceModel.EmployeeId.HasValue) throw new Exception();
                var employee1 = db.Employees.FirstOrDefault(e => e.Id == serviceModel.EmployeeId.Value);
                if (employee1 is null) throw new Exception();
                if (!serviceModel.HallId.HasValue && serviceModel.Address is null) throw new Exception();
                if (serviceModel.HallId.HasValue)
                {
                    var hall1 = db.Halls.Include(h => h.Address)
                        .FirstOrDefault(h => h.Id == serviceModel.HallId.Value);
                    if (hall1 is null) throw new Exception();
                    return new ApplicationService(service, employee1, serviceModel.StartDateTime.Value,
                        serviceModel.Duration.Value, hall1.Address, status);
                }

                var split = serviceModel.Address!.Split(" ");
                var address = db.Addresses.FirstOrDefault(a => a.Street == split[0] && a.HouseNumber == split[1]) ??
                              new Address(split[0], split[1]);
                return new ApplicationService(service, employee1, serviceModel.StartDateTime.Value,
                    serviceModel.Duration.Value, address, status);
            case 4:
                if (!serviceModel.StartDateTime.HasValue) throw new Exception();
                if (!serviceModel.Duration.HasValue) throw new Exception();
                if (!serviceModel.RentedItemId.HasValue) throw new Exception();
                var rented = db.RentedItems.FirstOrDefault(r => r.Id == serviceModel.RentedItemId.Value);
                if (rented is null) throw new Exception();
                if (!serviceModel.Number.HasValue) throw new Exception();
                return new ApplicationService(service, db.Employees.Single(e => e.RoleId == 7),
                    serviceModel.StartDateTime.Value,
                    serviceModel.Duration.Value, serviceModel.Number.Value, rented, status);
            case 5:
                if (!serviceModel.StartDateTime.HasValue) throw new Exception();
                if (!serviceModel.Duration.HasValue) throw new Exception();
                if (!serviceModel.IsFullTime.HasValue) throw new Exception();
                if (!serviceModel.EmployeeId.HasValue) throw new Exception();
                var employee2 = db.Employees.FirstOrDefault(e => e.Id == serviceModel.EmployeeId.Value);
                if (employee2 is null) throw new Exception();
                if (!serviceModel.HallId.HasValue && serviceModel.Address is null) throw new Exception();
                if (serviceModel.HallId.HasValue)
                {
                    var hall1 = db.Halls.Include(h => h.Address)
                        .FirstOrDefault(h => h.Id == serviceModel.HallId.Value);
                    if (hall1 is null) throw new Exception();
                    return new ApplicationService(service, employee2,
                        serviceModel.StartDateTime.Value,
                        serviceModel.Duration.Value, hall1.Address, serviceModel.IsFullTime.Value, status);
                }

                var split1 = serviceModel.Address!.Split(" ");
                var address1 = db.Addresses.FirstOrDefault(a => a.Street == split1[0] && a.HouseNumber == split1[1]) ??
                               new Address(split1[0], split1[1]);
                if (address1 is null) throw new Exception();
                return new ApplicationService(service, employee2, serviceModel.StartDateTime.Value,
                    serviceModel.Duration.Value, address1, serviceModel.IsFullTime.Value, status);
        }

        throw new Exception();
    }
}