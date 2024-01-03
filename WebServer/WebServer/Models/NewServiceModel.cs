using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PhotostudioDB;
using PhotostudioDB.Models;
using WebServer.Exceptions;

namespace WebServer.Models;

public class NewServiceModel
{
    [JsonPropertyName("serviceId")] public int ServiceId { get; set; }
    [JsonPropertyName("startDateTime")] public long? StartDateTime { get; set; }
    [JsonPropertyName("duration")] public int? Duration { get; set; }
    [JsonPropertyName("hallId")] public int? HallId { get; set; }
    [JsonPropertyName("employeeId")] public int? EmployeeId { get; set; }
    [JsonPropertyName("address")] public string? Address { get; set; }
    [JsonPropertyName("rentedItemId")] public int? RentedItemId { get; set; }
    [JsonPropertyName("number")] public int? Number { get; set; }
    [JsonPropertyName("isFullTime")] public bool? IsFullTime { get; set; }

    public static ApplicationService GetService(NewServiceModel serviceModel, ApplicationContext db)
    {
        var service = db.Services.FirstOrDefault(s => s.Id == serviceModel.ServiceId);
        if (service is null) throw new NewServiceException();
        var status = db.Statuses.First(st => st.Id == 7);
        switch (service.Type)
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
                if (!serviceModel.HallId.HasValue) throw new NewServiceException();
                var hall = db.Halls.FirstOrDefault(h => h.Id == serviceModel.HallId.Value);
                if (hall is null) throw new NewServiceException();
                if (!serviceModel.StartDateTime.HasValue) throw new NewServiceException();
                if (!serviceModel.Duration.HasValue) throw new NewServiceException();
                return new ApplicationService(service, db.Employees.Single(e => e.RoleId == 7),
                    new DateTime(1970, 1, 1, 3, 0, 0, 0).AddMilliseconds(serviceModel.StartDateTime.Value),
                    serviceModel.Duration.Value, hall, status);
            case 3:
                if (!serviceModel.StartDateTime.HasValue) throw new NewServiceException();
                if (!serviceModel.Duration.HasValue) throw new NewServiceException();
                if (!serviceModel.EmployeeId.HasValue) throw new NewServiceException();
                var employee1 = db.Employees.FirstOrDefault(e => e.Id == serviceModel.EmployeeId.Value);
                if (employee1 is null) throw new NewServiceException();
                if (!serviceModel.HallId.HasValue && serviceModel.Address is null) throw new NewServiceException();
                if (serviceModel.HallId.HasValue)
                {
                    var hall1 = db.Halls.Include(h => h.Address)
                        .FirstOrDefault(h => h.Id == serviceModel.HallId.Value);
                    if (hall1 is null) throw new NewServiceException();
                    return new ApplicationService(service, employee1, new DateTime(serviceModel.StartDateTime.Value),
                        serviceModel.Duration.Value, hall1.Address, status);
                }

                var split = serviceModel.Address!.Split(" ");
                var address = db.Addresses.FirstOrDefault(a => a.Street == split[0] && a.HouseNumber == split[1]) ??
                              new Address(split[0], split[1]);
                return new ApplicationService(service, employee1,
                    new DateTime(1970, 1, 1, 3, 0, 0, 0).AddMilliseconds(serviceModel.StartDateTime.Value),
                    serviceModel.Duration.Value, address, status);
            case 4:
                if (!serviceModel.StartDateTime.HasValue) throw new NewServiceException();
                if (!serviceModel.Duration.HasValue) throw new NewServiceException();
                if (!serviceModel.RentedItemId.HasValue) throw new NewServiceException();
                var rented = db.RentedItems.FirstOrDefault(r => r.Id == serviceModel.RentedItemId.Value);
                if (rented is null) throw new NewServiceException();
                if (!serviceModel.Number.HasValue) throw new NewServiceException();
                return new ApplicationService(service, db.Employees.Single(e => e.RoleId == 7),
                    new DateTime(1970, 1, 1, 3, 0, 0, 0).AddMilliseconds(serviceModel.StartDateTime.Value),
                    serviceModel.Duration.Value, serviceModel.Number.Value, rented, status);
            case 5:
                if (!serviceModel.StartDateTime.HasValue) throw new NewServiceException();
                if (!serviceModel.Duration.HasValue) throw new NewServiceException();
                if (!serviceModel.IsFullTime.HasValue) throw new NewServiceException();
                if (!serviceModel.EmployeeId.HasValue) throw new NewServiceException();
                var employee2 = db.Employees.FirstOrDefault(e => e.Id == serviceModel.EmployeeId.Value);
                if (employee2 is null) throw new NewServiceException();
                if (!serviceModel.HallId.HasValue && serviceModel.Address is null) throw new NewServiceException();
                if (serviceModel.HallId.HasValue)
                {
                    var hall1 = db.Halls.Include(h => h.Address)
                        .FirstOrDefault(h => h.Id == serviceModel.HallId.Value);
                    if (hall1 is null) throw new NewServiceException();
                    return new ApplicationService(service, employee2,
                        new DateTime(1970, 1, 1, 3, 0, 0, 0).AddMilliseconds(serviceModel.StartDateTime.Value),
                        serviceModel.Duration.Value, hall1.Address, serviceModel.IsFullTime.Value, status);
                }

                var split1 = serviceModel.Address!.Split(" ");
                var address1 = db.Addresses.FirstOrDefault(a => a.Street == split1[0] && a.HouseNumber == split1[1]) ??
                               new Address(split1[0], split1[1]);
                if (address1 is null) throw new NewServiceException();
                return new ApplicationService(service, employee2,
                    new DateTime(1970, 1, 1, 3, 0, 0, 0).AddMilliseconds(serviceModel.StartDateTime.Value),
                    serviceModel.Duration.Value, address1, serviceModel.IsFullTime.Value, status);
        }

        throw new NewServiceException();
    }

    public static IEnumerable<ApplicationService> GetServices(IEnumerable<NewServiceModel> serviceModels,
        ApplicationContext db)
    {
        return serviceModels.Select(s => GetService(s, db));
    }
}