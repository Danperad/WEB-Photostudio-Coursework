using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Controllers;

[ApiController]
[Route("halls")]
public class HallController(IHallService hallService) : ControllerBase
{
    [HttpGet("available")]
    public IActionResult GetAvailableHalls(DateTime start, int duration)
    {
        start = start.Date + TimeSpan.FromMinutes(start.Minute + start.Hour * 60);
        var halls = hallService.GetAvailableHalls(start, TimeSpan.FromMinutes(duration));
        return Ok(halls);
    }
}