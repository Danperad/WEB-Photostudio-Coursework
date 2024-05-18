using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Controllers;

[ApiController]
[Route("halls")]
public class HallController(IHallService hallService) : ControllerBase
{
    [HttpGet("available")]
    public async Task<IActionResult> GetAvailableHalls(DateTime start, int duration)
    {
        var halls = await hallService.GetAvailableHalls(start, TimeSpan.FromMinutes(duration));
        return Ok(halls);
    }
}