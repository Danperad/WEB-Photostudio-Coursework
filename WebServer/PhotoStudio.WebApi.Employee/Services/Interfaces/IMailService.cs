namespace PhotoStudio.WebApi.Employee.Services.Interfaces;

public interface IMailService
{
    Task NotifyClientNewOrder(int orderId, CancellationToken stoppingToken);
    Task NotifyClientOrderStatusChanged(int orderId, CancellationToken stoppingToken);
}