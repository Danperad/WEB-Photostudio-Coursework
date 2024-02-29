using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace PhotoStudio.WebApi.Employee.Controllers;

[Authorize]
public class OrderHub : Hub
{
    [Authorize]
    public async Task Send()
    {
        var users = new List<string>();
        var clients = Clients.Users(users);
    }
}