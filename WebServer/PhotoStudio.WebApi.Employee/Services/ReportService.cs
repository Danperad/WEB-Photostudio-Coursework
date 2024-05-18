using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NPOI.XSSF.UserModel;
using PhotoStudio.DataBase;
using PhotoStudio.WebApi.Employee.Dto;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Services;

public class ReportService(PhotoStudioContext context, IMapper mapper) : IReportService
{
    public async Task GenReport(DateTime startDate, DateTime endDate)
    {
        var startDateOnly = startDate.Date;
        var endDateOnly = endDate.Date;
        var orders = await context.Orders.Where(o => o.DateTime >= startDateOnly && o.DateTime <= endDateOnly)
            .ProjectTo<OrderReportDto>(mapper.ConfigurationProvider).ToListAsync();
        using var workbook = new XSSFWorkbook();
        var sheet = workbook.CreateSheet("Report");
        for (var i = 0; i < orders.Count; i++)
        {
            var row = sheet.CreateRow(i);
            row.CreateCell(0).SetCellValue(i+1);
            row.CreateCell(1).SetCellValue(orders[i].Number);
            row.CreateCell(2).SetCellValue(DateOnly.FromDateTime(orders[i].DateTime));
            row.CreateCell(3).SetCellValue(orders[i].Status);
            row.CreateCell(4).SetCellValue(Convert.ToDouble(orders[i].TotalPrice));
        }
    }
}