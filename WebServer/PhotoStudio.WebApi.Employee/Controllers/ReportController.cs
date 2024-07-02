using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Controllers;

[ApiController]
[Route("reports")]
public class ReportController(IReportService reportService) : ControllerBase
{
    [HttpGet]
    public async Task<FileResult> GetDatedReport(DateTime startDate, DateTime endDate)
    {
        HttpContext.Response.Headers.AccessControlExposeHeaders = new StringValues(HttpContext.Response.Headers
            .AccessControlExposeHeaders.Append("Content-Disposition").ToArray());
        var outputStream = new MemoryStream();
        await reportService.GenReport(startDate, endDate, outputStream);
        var array = outputStream.ToArray();
        return File(array, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"report-{startDate:ddMMyy}-{endDate:ddMMyy}.xlsx");
    }
}