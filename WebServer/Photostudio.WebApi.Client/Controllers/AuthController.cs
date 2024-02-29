using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;

namespace PhotoStudio.WebApi.Client.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IClientService clientService) : ControllerBase
{
    [HttpPost("signin")]
    public async Task<IActionResult> LoginUser(AuthModel authModel)
    {
        if (string.IsNullOrWhiteSpace(authModel.Login) || string.IsNullOrWhiteSpace(authModel.Password))
        {
            return BadRequest(new AnswerDto(false, null, 400));
        }

        var client = await clientService.AuthClientAsync(authModel);
        if (client is null)
        {
            return BadRequest(new AnswerDto(false, null, 101));
        }

        return Ok(new AnswerDto(true, client, null));
    }

    [HttpPost("signup")]
    public async Task<IActionResult> RegisterUser(
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)]
        RegisterDto registerDto)
    {
        try
        {
            var client = await clientService.RegisterClientAsync(registerDto);
            return Ok(new AnswerDto(true, client, null));
        }
        catch (NotImplementedException e)
        {
            return Ok(new AnswerDto(false, null, Convert.ToInt32(e.Message)));
        }
    }

    [HttpGet("reauth")]
    public async Task<IActionResult> ReAuthUser([FromQuery] string token)
    {
        try
        {
            var client = await clientService.ReAuthClientAsync(token);
            return Ok(new AnswerDto(true, client, null));
        }
        catch (NotImplementedException e)
        {
            return Ok(new AnswerDto(false, null, Convert.ToInt32(e.Message)));
        }
    }
}