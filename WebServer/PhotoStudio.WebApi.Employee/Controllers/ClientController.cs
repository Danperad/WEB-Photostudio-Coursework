using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Employee.Dto;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Controllers;

[ApiController]
[Route("clients")]
public class ClientController(IClientService clientService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllClients(string? search = null, int? number = null, int? start = null)
    {
        var clients = clientService.GetClients(search, number ?? 20, start ?? 0);
        return Ok(clients);
    }

    [HttpPost]
    public async Task<IActionResult> AddClient(NewClientDto clientDto)
    {
        var newClient = await clientService.AddNewClient(clientDto);
        return Ok(newClient);
    }
}