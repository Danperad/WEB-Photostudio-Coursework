using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;

namespace PhotoStudio.WebApi.Client.Controllers;

[ApiController]
[Route("employee")]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    [HttpGet("getfree")]
    public async Task<IActionResult> GetFreeEmployees([FromQuery] int service, [FromQuery] DateTime start,
        [FromQuery] int duration)
    {
        if (start < DateTime.Now.AddDays(1) || duration <= 30)
        {
            return Ok(new AnswerDto(false, null, 101));
        }

        var employees = await employeeService.GetAvailableEmployeesAsync(start, duration, service);
        return Ok(new AnswerDto(true, new { employees }, null));
    }
}