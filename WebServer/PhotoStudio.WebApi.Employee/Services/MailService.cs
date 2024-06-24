using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Services;

public class MailService : IMailService
{
    private PhotoStudioContext _context { get; set; }
    private IServiceScope _scope { get; set; }

    public MailService(IServiceScopeFactory scopeFactory)
    {
        _scope = scopeFactory.CreateScope();
        _context = _scope.ServiceProvider.GetRequiredService<PhotoStudioContext>();
    }

    ~MailService()
    {
        _scope.Dispose();
    }

    public Task NotifyClientNewOrder(int orderId, CancellationToken stoppingToken)
    {
        var order = _context.Orders.AsNoTracking().Where(o => o.Id == orderId);
        return Task.CompletedTask;
    }

    public Task NotifyClientOrderStatusChanged(int orderId, CancellationToken stoppingToken)
    {
        var order = _context.Orders.AsNoTracking().Where(o => o.Id == orderId);
        return Task.CompletedTask;
    }
}