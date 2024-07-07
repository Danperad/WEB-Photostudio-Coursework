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
    public async Task<IActionResult> GetFree([FromQuery] DateTime start, [FromQuery] int duration, [FromQuery] int type)
    {
        if (start < DateTime.Now.AddDays(1) || duration == 0)
        {
            return BadRequest(new AnswerDto(false, null, 101));
        }

        var items = await rentedItemService.GetAvailableItemsByServiceTypeAsync(start, duration, type);
        return Ok(new AnswerDto(true, new {items}, null));
    }
}