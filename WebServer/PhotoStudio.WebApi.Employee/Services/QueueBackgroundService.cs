using System.Text;
using Microsoft.AspNetCore.SignalR;
using PhotoStudio.WebApi.Employee.Hubs;
using PhotoStudio.WebApi.Employee.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PhotoStudio.WebApi.Employee.Services;

public class QueueBackgroundService(IConfiguration configuration, IHubContext<MainHub> mainHub, IMailService mailService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = configuration.GetSection("RabbitMq")["HostName"],
            Port = Convert.ToInt32(configuration.GetSection("RabbitMq")["Port"]),
            UserName = configuration.GetSection("RabbitMq")["UserName"],
            Password = configuration.GetSection("RabbitMq")["Password"],
            ClientProvidedName = "PhotostudioEmployee",
            DispatchConsumersAsync = true
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "orders",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += async (_, ea) =>
        {
            var body = Encoding.UTF8.GetString(ea.Body.ToArray());
            if (body.StartsWith("New_Order"))
            {
                var @params = body.Split(' ');
                await mainHub.Clients.All.SendAsync("NewOrder", stoppingToken);
                await mailService.NotifyClientNewOrder(int.Parse(@params[1]), stoppingToken);
            }

            if (body.StartsWith("Order_Status_Changed"))
            {
                var @params = body.Split(' ');
                await mainHub.Clients.All.SendAsync("StatusChanged", @params[1], stoppingToken);
                await mailService.NotifyClientOrderStatusChanged(int.Parse(@params[1]), stoppingToken);
            }

            await Task.Yield();
        };
        channel.BasicConsume("orders", true, consumer);
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
        }
    }
}