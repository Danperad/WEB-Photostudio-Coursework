using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Controllers;

[ApiController]
[Route("employees")]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    [HttpGet("available")]
    public IActionResult GetAvailableEmployee(DateTime start, int duration, int serviceId)
    {
        start = start.Date + TimeSpan.FromMinutes(start.Minute + start.Hour * 60);
        var employees = employeeService.GetAvailableEmployees(start, TimeSpan.FromMinutes(duration), serviceId);
        return Ok(employees);
    }

    [HttpGet]
    public IActionResult GetEmployees()
    {
        var employees = employeeService.GetAllEmployees();
        return Ok(employees);
    }
}