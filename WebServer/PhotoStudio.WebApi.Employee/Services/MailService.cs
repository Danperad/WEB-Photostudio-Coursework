using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Services;

public class MailService : IMailService
{
    private PhotoStudioContext Context { get; set; }
    private IServiceScope Scope { get; set; }

    public MailService(IServiceScopeFactory scopeFactory)
    {
        Scope = scopeFactory.CreateScope();
        Context = Scope.ServiceProvider.GetRequiredService<PhotoStudioContext>();
    }

    ~MailService()
    {
        Scope.Dispose();
    }

    public Task NotifyClientNewOrder(int orderId, CancellationToken stoppingToken)
    {
        var order = Context.Orders.AsNoTracking().Where(o => o.Id == orderId);
        Debug.WriteLine(order.ToString());
        return Task.CompletedTask;
    }

    public Task NotifyClientOrderStatusChanged(int orderId, CancellationToken stoppingToken)
    {
        var order = Context.Orders.AsNoTracking().Where(o => o.Id == orderId);
        Debug.WriteLine(order.ToString());
        return Task.CompletedTask;
    }
}