using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Controllers;

// [Authorize]
public class ClientHub(IClientService clientService) : Hub
{
    public override Task OnConnectedAsync()
    {
        Groups.AddToGroupAsync(Context.ConnectionId, "Clients");
        return base.OnConnectedAsync();
    }

    public async Task Send(string? search = null, int count = 10, int start = 0)
    {
        var clients = await clientService.GetClients(search, count, start);
        var json = JsonSerializer.Serialize(clients);
        await Clients.Caller.SendAsync("Receive", json);
    }

    public async Task Updated()
    {
        await Clients.Group("Clients").SendAsync("Update", "Client list changed");
    }

    public async Task Ping(string text)
    {
        var json = JsonSerializer.Deserialize<Testtttt>(text);
        json.message += " Pong";
        await Clients.Caller.SendAsync("Pong", JsonSerializer.Serialize(json));
    }

    class Testtttt
    {
        public string message { get; set; }
    }
}