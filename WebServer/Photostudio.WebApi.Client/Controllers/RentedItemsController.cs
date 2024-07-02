using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;

namespace PhotoStudio.WebApi.Client.Controllers;

[ApiController]
[Route("renteditems")]
public class RentedItemsController(IRentedItemService rentedItemService) : ControllerBase
{
    [HttpGet("get")]
    public async Task<IActionResult> GetItems([FromQuery] int type)
    {
        var items = await rentedItemService.GetItemsByServiceTypeAsync(type);
        return Ok(new AnswerDto(true, new {items}, null));
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
        return Ok(new AnswerDto(true, new {items}, null));
    }
}