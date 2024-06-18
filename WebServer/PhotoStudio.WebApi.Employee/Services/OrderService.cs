using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Employee.Dto;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Services;

public class OrderService(PhotoStudioContext context, IRabbitMqService rabbitMqService, IMapper mapper) : IOrderService
{
    public IAsyncEnumerable<OrderDto> GetAllOrders(int count, int start)
    {
        var orders = context.Orders.AsNoTracking().OrderByDescending(o => o.Id).Skip(start).Take(count)
            .Include(o => o.Client);
        return orders.ProjectTo<OrderDto>(mapper.ConfigurationProvider).AsAsyncEnumerable();
    }

    public IAsyncEnumerable<OrderDto> GetOrdersByClient(int clientId)
    {
        var client = context.Clients.AsNoTracking().OrderByDescending(o => o.Id).Include(c => c.Orders)
            .Where(c => c.Id == clientId).SelectMany(c => c.Orders).OrderByDescending(o => o.Id)
            .ProjectTo<OrderDto>(mapper.ConfigurationProvider).AsAsyncEnumerable();
        return client;
    }

    public async Task<OrderDto> AddNewOrder(NewOrderDto order)
    {
        if (order.Services.Count == 0 && order.ServicePackage is null)
        {
            throw new NotImplementedException();
        }

        foreach (var service in order.Services)
        {
            if (service.StartDateTime.HasValue)
            {
                service.StartDateTime = service.StartDateTime.Value.Date +
                                        TimeSpan.FromMinutes(service.StartDateTime.Value.Minute +
                                                             service.StartDateTime.Value.Hour * 60);
            }
        }

        var client = await context.Clients.FirstAsync(c => c.Id == order.Client);

        var services = new List<ApplicationService>(order.Services.Count);
        var status =
            await context.Statuses.SingleAsync(s => s.Id == StatusValue.NotAccepted && s.Type == StatusType.Service);
        ApplicationService? hallService = null;
        ServicePackage? servicePackage = null;
        if (order.ServicePackage is not null)
        {
            servicePackage = await context.ServicePackages.Include(sp => sp.Services)
                .ThenInclude(applicationServiceTemplate => applicationServiceTemplate.Service)
                .FirstAsync(s => s.Id == order.ServicePackage.ServicePackageId);
            foreach (var serviceTemplate in servicePackage.Services)
            {
                switch (serviceTemplate.Service.Type)
                {
                    case Service.ServiceType.Simple:
                    {
                        var employee = await context.Employees
                            .Where(e => e.BoundServices.Contains(serviceTemplate.Service))
                            .OrderBy(_ => Guid.NewGuid()).FirstAsync();
                        services.Add(serviceTemplate.MapToApplicationService(employee));
                        break;
                    }
                    case Service.ServiceType.HallRent or Service.ServiceType.ItemRent:
                    {
                        var employee = await context.Employees
                            .Where(e => e.BoundServices.Contains(serviceTemplate.Service))
                            .OrderBy(_ => Guid.NewGuid()).FirstAsync();
                        var newService =
                            serviceTemplate.MapToApplicationService(employee, order.ServicePackage.StartDateTime);
                        services.Add(
                            newService);
                        if (serviceTemplate.Service.Type == Service.ServiceType.HallRent)
                            hallService = newService;
                        break;
                    }
                    default:
                        services.Add(serviceTemplate.MapToApplicationService(null, order.ServicePackage.StartDateTime));
                        break;
                }
            }
        }

        foreach (var newServiceModel in order.Services.OrderBy(s => s.Id))
        {
            var service = await context.Services.SingleAsync(s => s.Id == newServiceModel.Service);
            if (services.Any(s => s.Service == service) &&
                service.Type is Service.ServiceType.HallRent or Service.ServiceType.Photo)
                throw new NotImplementedException();
            switch (service.Type)
            {
                case Service.ServiceType.Simple:
                {
                    var newService = new ApplicationService(service, status)
                    {
                        Employee = context.Employees.Where(e => e.BoundServices.Contains(service))
                            .OrderBy(_ => Guid.NewGuid()).First()
                    };
                    services.Add(newService);
                }
                    break;
                case Service.ServiceType.HallRent:
                {
                    CheckServiceModelPresent(newServiceModel);
                    var hall = await context.Halls.FirstAsync(h => h.Id == newServiceModel.Hall!);
                    var newService = new ApplicationService(service, newServiceModel.StartDateTime!.Value,
                        TimeSpan.FromMinutes(newServiceModel.Duration!.Value), hall, status)
                    {
                        Employee = context.Employees.Where(e => e.BoundServices.Contains(service))
                            .OrderBy(_ => Guid.NewGuid()).First()
                    };
                    hallService = newService;
                    services.Add(newService);
                }
                    break;
                case Service.ServiceType.Photo:
                {
                    CheckServiceModelPresent(newServiceModel);
                    var employee = await context.Employees
                        .FirstAsync(e => e.Id == newServiceModel.Employee);
                    var newService = new ApplicationService(service, employee, newServiceModel.StartDateTime!.Value,
                        TimeSpan.FromMinutes(newServiceModel.Duration!.Value), status);
                    services.Add(newService);
                }
                    break;
                case Service.ServiceType.ItemRent:
                {
                    CheckServiceModelPresent(newServiceModel);
                    if (!newServiceModel.Count.HasValue)
                        throw new NotImplementedException();
                    var item = await context.RentedItems
                        .FirstAsync(i => i.Id == newServiceModel.Item);
                    var newService = new ApplicationService(service, newServiceModel.StartDateTime!.Value,
                        TimeSpan.FromMinutes(newServiceModel.Duration!.Value), newServiceModel.Count.Value, item,
                        status)
                    {
                        Employee = context.Employees.Where(e => e.BoundServices.Contains(service))
                            .OrderBy(_ => Guid.NewGuid()).First()
                    };
                    services.Add(newService);
                }
                    break;
                case Service.ServiceType.Style:
                {
                    CheckServiceModelPresent(newServiceModel);
                    if (!newServiceModel.IsFullTime.HasValue)
                        throw new NotImplementedException();
                    var employee = await context.Employees
                        .FirstAsync(e => e.Id == newServiceModel.Employee);
                    var newService = new ApplicationService(service, employee, newServiceModel.StartDateTime!.Value,
                        TimeSpan.FromMinutes(newServiceModel.Duration!.Value), newServiceModel.IsFullTime.Value,
                        status);
                    services.Add(newService);
                }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        if (hallService is null &&
            services.Any(s => s.Service.Type is Service.ServiceType.Photo or Service.ServiceType.Style))
        {
            throw new NotImplementedException();
        }

        foreach (var applicationService in services.Where(s =>
                     s.Service.Type is Service.ServiceType.Photo or Service.ServiceType.Style))
        {
            applicationService.Hall = hallService!.Hall;
        }

        var orderStatus =
            await context.Statuses.SingleAsync(s => s.Id == StatusValue.NotAccepted && s.Type == StatusType.Order);

        var newOrder = new Order(client, DateTime.Now, services, orderStatus, servicePackage);
        var newAddedOrder = await context.Orders.AddAsync(newOrder);

        await context.SaveChangesAsync();
        rabbitMqService.SendMessage("New_Order");
        return mapper.Map<Order, OrderDto>(newAddedOrder.Entity);
    }


    private static void CheckServiceModelPresent(NewOrderServiceDto serviceModel)
    {
        if (!serviceModel.StartDateTime.HasValue || !serviceModel.Duration.HasValue)
            throw new NotImplementedException();
    }

    public async Task<OrderDto> ChangeOrderStatus(int orderId, int statusId)
    {
        var order = await context.Orders.Include(order => order.Services).Include(order => order.Status)
            .FirstAsync(o => o.Id == orderId);
        if (order.Status.Id is StatusValue.Canceled or StatusValue.Done)
            throw new NotImplementedException();
        var newStatus =
            await context.Statuses.FirstAsync(s => s.Type == StatusType.Order && s.Id == (StatusValue)statusId);
        order.Status = newStatus;
        switch ((StatusValue)statusId)
        {
            case StatusValue.Canceled:
            {
                var serviceStatus = await
                    context.Statuses.FirstAsync(s => s.Type == StatusType.Service && s.Id == StatusValue.Canceled);
                foreach (var service in order.Services)
                {
                    service.Status = serviceStatus;
                }
            }
                break;
            case StatusValue.InWork:
            {
                var serviceStatus = await
                    context.Statuses.FirstAsync(s => s.Type == StatusType.Service && s.Id == StatusValue.NotStarted);
                foreach (var service in order.Services)
                {
                    service.Status = serviceStatus;
                }
            }
                break;
            case StatusValue.NotAccepted:
            case StatusValue.Done:
            case StatusValue.NotStarted:
            default:
                throw new ArgumentOutOfRangeException(nameof(statusId), statusId, null);
        }

        var newOrder = context.Orders.Entry(order);
        await context.SaveChangesAsync();
        return mapper.Map<Order, OrderDto>(newOrder.Entity);
    }
}