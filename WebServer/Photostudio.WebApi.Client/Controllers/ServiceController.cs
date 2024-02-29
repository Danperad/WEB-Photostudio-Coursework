using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;

namespace PhotoStudio.WebApi.Client.Controllers;

[ApiController]
[Route("services")]
public class ServiceController(IServiceService serviceService) : ControllerBase
{
    [HttpGet("get")]
    public async Task<IActionResult> GetServices([FromQuery] int? count = null, [FromQuery] int? start = null,
        [FromQuery] int? order = null, [FromQuery] int? type = null, [FromQuery] string? search = null)
    {
        var services = await serviceService.GetAllServicesAsync(count, start, order, type, search);
        var serviceDtos = services as ServiceDto[] ?? services.ToArray();
        return Ok(new AnswerDto(true, new { services = serviceDtos, hasMore = serviceDtos.Length != 0 }, null));
    }
}