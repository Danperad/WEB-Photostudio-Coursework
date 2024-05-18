using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Controllers;

[ApiController]
[Route("employees")]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    [HttpGet("available")]
    public async Task<IActionResult> GetAvailableEmployee(DateTime start, int duration, int serviceId)
    {
        var employees = await employeeService.GetAvailableEmployees(start, TimeSpan.FromMinutes(duration), serviceId);
        return Ok(employees);
    }
}