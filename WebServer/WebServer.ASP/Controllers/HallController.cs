using Microsoft.AspNetCore.Mvc;
using WebServer.ASP.Dto;
using WebServer.ASP.Services.Interfaces;

namespace WebServer.ASP.Controllers;

[ApiController]
[Route("hall")]
public class HallController(IHallService hallService) : ControllerBase
{
    [HttpGet("get")]
    public async Task<IActionResult> GetHalls()
    {
        var halls = await hallService.GetHallsAsync();
        return Ok(new AnswerDto(true, new { halls = halls }, null));
    }

    [HttpGet("getfree")]
    public async Task<IActionResult> GetFree([FromQuery] DateTime start, [FromQuery] int duration)
    {
        if (start < DateTime.Now.AddDays(1) || duration < 60)
        {
            return Ok(new AnswerDto(false, null, 101));
        }
        var halls = await hallService.GetAvailableHallsAsync(start, duration);
        return Ok(new AnswerDto(true, new { halls = halls }, null));
    }
}