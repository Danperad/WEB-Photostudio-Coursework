using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;

namespace PhotoStudio.WebApi.Client.Controllers;

[ApiController]
[Route("client")]
public class ClientController(IClientService clientService, IMapper mapper) : ControllerBase
{
    [HttpPost("update")]
    public async Task<IActionResult> UpdateClient(ClientDto clientDto)
    {
        var client = await clientService.AuthClientInContextAsync(HttpContext.User);
        var newClient = await clientService.UpdateClientAsync(clientDto, client);
        return Ok(new AnswerDto(true, new
        {
            user = newClient
        }, null));
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetClient()
    {
        var client = await clientService.AuthClientInContextAsync(HttpContext.User);
        return Ok(new AnswerDto(true, new
        {
            user = mapper.Map<ClientDto>(client)
        }, null));
    }

    [HttpPost("buy")]
    public async Task<IActionResult> AddOrder(NewOrderModel cart)
    {
        var client = await clientService.AuthClientInContextAsync(HttpContext.User);
        var res = await clientService.AddOrderAsync(cart, client);
        return Ok(new AnswerDto(res, null, null));
    }
}