namespace PhotoStudio.WebApi.Client.Services.Interfaces;

public interface IRabbitMqService
{
    void SendMessage(object obj);
    void SendMessage(string message);
}