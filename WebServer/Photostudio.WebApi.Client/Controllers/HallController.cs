using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;

namespace PhotoStudio.WebApi.Client.Controllers;

[ApiController]
[Route("hall")]
public class HallController(IHallService hallService) : ControllerBase
{
    [HttpGet("get")]
    public async Task<IActionResult> GetHalls()
    {
        var halls = await hallService.GetHallsAsync();
        return Ok(new AnswerDto(true, new { halls }, null));
    }

    [HttpGet("getfree")]
    public async Task<IActionResult> GetFree([FromQuery] DateTime start, [FromQuery] int duration)
    {
        if (start < DateTime.Now.AddDays(1) || duration <= 30)
        {
            return Ok(new AnswerDto(false, null, 101));
        }
        var halls = await hallService.GetAvailableHallsAsync(start, duration);
        return Ok(new AnswerDto(true, new { halls }, null));
    }
}