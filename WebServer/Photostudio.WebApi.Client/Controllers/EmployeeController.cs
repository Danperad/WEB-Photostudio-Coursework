using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;

namespace PhotoStudio.WebApi.Client.Controllers;

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