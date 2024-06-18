using System.Text;
using System.Text.Json;
using PhotoStudio.WebApi.Employee.Services.Interfaces;
using RabbitMQ.Client;

namespace PhotoStudio.WebApi.Employee.Services;

public class RabbitMqService(IConfiguration configuration) : IRabbitMqService
{
    public void SendMessage(object obj)
    {
        var message = JsonSerializer.Serialize(obj);
        SendMessage(message);
    }

    public void SendMessage(string message)
    {
        var factory = new ConnectionFactory
        {
            HostName = configuration.GetSection("RabbitMq")["HostName"],
            Port = Convert.ToInt32(configuration.GetSection("RabbitMq")["Port"]),
            UserName = configuration.GetSection("RabbitMq")["UserName"],
            Password = configuration.GetSection("RabbitMq")["Password"],
            ClientProvidedName = "PhotoStudioEmployee",
            DispatchConsumersAsync = true
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "orders",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: string.Empty,
            routingKey: "orders",
            basicProperties: null,
            body: body);
    }
}