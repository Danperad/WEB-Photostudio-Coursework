using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Controllers;

[ApiController]
[Route("items")]
public class RentedItemController(IRentedItemService rentedItemService) : ControllerBase
{
    [HttpGet("available")]
    public async Task<IActionResult> GetAvailableItems(DateTime start, int duration)
    {
        var items = await rentedItemService.GetAvailableItems(start, TimeSpan.FromMinutes(duration));
        return Ok(items);
    }
}