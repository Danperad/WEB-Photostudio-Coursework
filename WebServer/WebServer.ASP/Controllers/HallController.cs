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
    public async Task<IActionResult> GetFree([FromQuery] ulong start, [FromQuery] int duration)
    {
        if (start == 0 || duration == 0)
        {
            return Ok(new AnswerDto(false, null, 101));
        }

        var date = new DateTime(1970, 1, 1, 3, 0, 0, 0).AddMilliseconds(start);
        var halls = await hallService.GetAvailableHallsAsync(date, duration);
        return Ok(new AnswerDto(true, new { halls = halls }, null));
    }
}