using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Controllers;

[ApiController]
[Route("packages")]
public class ServicePackageController(IServicePackageService servicePackageService) : ControllerBase
{
    [HttpGet("available")]
    public IActionResult GetAvailableServicePackages(DateTime start)
    {
        start = start.Date + TimeSpan.FromMinutes(start.Minute + start.Hour * 60);
        var items = servicePackageService.GetAvailableServicePackages(start);
        return Ok(items);
    }
}