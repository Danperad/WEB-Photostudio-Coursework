using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Controllers;

[ApiController]
[Route("services")]
public class ServiceController(IServiceService serviceService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllServices()
    {
        var services = serviceService.GetServices();
        return Ok(services);
    }
}