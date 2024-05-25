using Microsoft.AspNetCore.Mvc;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Controllers;

[ApiController]
[Route("reports")]
public class ReportController(IReportService reportService) : ControllerBase
{
    [HttpGet]
    public async Task<FileResult> GetDatedReport(DateTime startDate, DateTime endDate)
    {
        var outputStream = new MemoryStream();
        await reportService.GenReport(startDate, endDate, outputStream);
        var array = outputStream.ToArray();
        return File(array, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"report-{startDate:ddMMyy}-{endDate:ddMMyy}.xlsx");
    }
}