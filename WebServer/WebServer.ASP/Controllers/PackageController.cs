using Microsoft.AspNetCore.Mvc;
using WebServer.ASP.Dto;
using WebServer.ASP.Services.Interfaces;

namespace WebServer.ASP.Controllers;

[ApiController]
[Route("package")]
public class PackageController(IPackageService packageService) : ControllerBase
{
    [HttpGet("get")]
    public async Task<IActionResult> GetPackages()
    {
        var packages = await packageService.GetAllPackagesAsync();
        return Ok(new AnswerDto(true, new {packages = packages}, null));
    }
}