using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;

namespace PhotoStudio.WebApi.Client.Controllers;

[ApiController]
[Route("package")]
public class PackageController(IPackageService packageService) : ControllerBase
{
    [HttpGet("get")]
    public async Task<IActionResult> GetPackages()
    {
        var packages = await packageService.GetAllPackagesAsync();
        return Ok(new AnswerDto(true, new {packages}, null));
    }
}