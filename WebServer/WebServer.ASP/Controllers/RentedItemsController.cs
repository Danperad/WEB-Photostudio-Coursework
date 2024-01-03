using Microsoft.AspNetCore.Mvc;
using WebServer.ASP.Dto;
using WebServer.ASP.Services.Interfaces;

namespace WebServer.ASP.Controllers;

[ApiController]
[Route("renteditems")]
public class RentedItemsController(IRentedItemService rentedItemService) : ControllerBase
{
    [HttpGet("get")]
    public async Task<IActionResult> GetItems([FromQuery] int type)
    {
        var items = await rentedItemService.GetItemsByServiceTypeAsync(type);
        return Ok(new AnswerDto(true, new {items = items}, null));
    }

    [HttpGet("getfree")]
    public async Task<IActionResult> GetFree([FromQuery] long start, [FromQuery] int duration, [FromQuery] int type)
    {
        if (start == 0 || duration == 0)
        {
            return BadRequest(new AnswerDto(false, null, 101));
        }

        var date = new DateTime(1970, 1, 1, 3, 0, 0, 0).AddMilliseconds(start);
        var items = await rentedItemService.GetAvailableItemsByServiceTypeAsync(date, duration, type);
        return Ok(new AnswerDto(true, new {items = items}, null));
    }
}