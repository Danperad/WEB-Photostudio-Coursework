using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Controllers;

[ApiController]
[Route("items")]
public class RentedItemController(IRentedItemService rentedItemService) : ControllerBase
{
    [HttpGet("available")]
    public IActionResult GetAvailableItems(DateTime start, int duration)
    {
        start = start.Date + TimeSpan.FromMinutes(start.Minute + start.Hour * 60);
        var items = rentedItemService.GetAvailableItems(start, TimeSpan.FromMinutes(duration));
        return Ok(items);
    }
}