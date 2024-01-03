using Microsoft.AspNetCore.Mvc;
using WebServer.ASP.Dto;
using WebServer.ASP.Services.Interfaces;

namespace WebServer.ASP.Controllers;

[ApiController]
[Route("employee")]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    [HttpGet("getfree")]
    public async Task<IActionResult> GetFreeEmployees([FromQuery] int service, [FromQuery] long start,
        [FromQuery] int duration)
    {
        if (start == 0 || duration == 0)
        {
            return Ok(new AnswerDto(false, null, 101));
        }

        var date = new DateTime(1970, 1, 1, 3, 0, 0, 0).AddMilliseconds(start);
        var employees = await employeeService.GetAvailableEmployeesAsync(date, duration, service);
        return Ok(new AnswerDto(true, new { employees = employees }, null));
    }
}