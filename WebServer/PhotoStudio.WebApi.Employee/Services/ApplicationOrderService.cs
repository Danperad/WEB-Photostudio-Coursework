using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Employee.Dto;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Services;

public class ApplicationOrderService(PhotoStudioContext context, IRabbitMqService rabbitMqService, IMapper mapper)
    : IApplicationOrderService
{
    public async Task<List<OrderServiceWithClientDto>> GetServicesByEmployee(int employeeId, bool showAll)
    {
        var services = await context.Employees.Where(e => e.Id == employeeId).Take(1).Include(e => e.Services)
            .ThenInclude(applicationService => applicationService.Order).ThenInclude(order => order.Status)
            .SelectMany(e => e.Services)
            .Where(s => showAll ||
                        ((s.Order.StatusId == StatusValue.InWork || s.Order.StatusId == StatusValue.NotStarted) && (s.StatusId == StatusValue.NotStarted || s.StatusId == StatusValue.InWork)))
            .ProjectTo<OrderServiceWithClientDto>(mapper.ConfigurationProvider)
            .ToListAsync();
        return services;
    }

    public async Task<List<OrderServiceWithClientDto>> GetServicesByOrder(int orderId)
    {
        var services = await context.Orders.Where(o => o.Id == orderId).Include(o => o.Services).Include(o => o.Status)
            .SelectMany(o => o.Services).ProjectTo<OrderServiceWithClientDto>(mapper.ConfigurationProvider)
            .ToListAsync();
        return services;
    }

    public async Task<OrderServiceWithClientDto> UpdateServiceStatus(int orderServiceId, int statusId)
    {
        var service = await context.ApplicationServices.Include(s => s.Order).ThenInclude(o => o.Services)
            .ThenInclude(applicationService => applicationService.Status)
            .Include(s => s.Order).ThenInclude(order => order.Status)
            .FirstAsync(a => a.Id == orderServiceId);
        if (service.Order.Status.Id is StatusValue.Done or StatusValue.Canceled)
            throw new NotImplementedException();
        if (service.Order.Status.Id is StatusValue.NotAccepted)
            throw new NotImplementedException();
        var status =
            await context.Statuses.FirstAsync(s => s.Type == StatusType.Service && s.Id == (StatusValue)statusId);
        if (status.Id is StatusValue.Canceled or StatusValue.NotAccepted or StatusValue.NotStarted)
            throw new NotImplementedException();
        service.Status = status;
        var flag = false;
        if (status.Id is StatusValue.Done && service.Order.Services.All(s => s.Status.Id is StatusValue.Done))
        {
            flag = true;
            service.Order.Status =
                await context.Statuses.FirstAsync(s => s.Type == StatusType.Order && s.Id == StatusValue.Done);
        }

        var newService = context.ApplicationServices.Entry(service);
        await context.SaveChangesAsync();
        rabbitMqService.SendMessage($"Service_Status_Changed {orderServiceId}");
        if (flag)
            rabbitMqService.SendMessage($"Order_Status_Changed {service.Order.Id}");
        return mapper.Map<ApplicationService, OrderServiceWithClientDto>(newService.Entity);
    }
}