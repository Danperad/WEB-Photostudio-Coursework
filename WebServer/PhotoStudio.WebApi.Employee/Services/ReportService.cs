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
            var order = orders[i];
            var row = sheet.CreateRow(0);
            row.CreateCell(1).SetCellValue(i + 1);
            row.CreateCell(2).SetCellValue(order.Number);
            row.CreateCell(3).SetCellValue(DateOnly.FromDateTime(order.DateTime));
            row.CreateCell(4).SetCellValue(order.Client.LastName);
            row.CreateCell(5).SetCellValue(order.Status);
            row.CreateCell(6).SetCellValue(order.ServicePackage?.Title ?? "-");
            row.CreateCell(7).SetCellValue(Convert.ToDouble(order.TotalPrice));
            foreach (var service in order.Services)
            {
                row.CreateCell(9).SetCellValue(service.Service);
                row.CreateCell(9).SetCellValue(service.Employee.LastName);
                row.CreateCell(10).SetCellValue(service.Status);
                row.CreateCell(11).SetCellValue(service.Hall ?? "-");
                row.CreateCell(12).SetCellValue(service.Item ?? "-");
                {
                    var cell = row.CreateCell(13);
                    if (service.Number.HasValue)
                        cell.SetCellValue(service.Number.Value);
                    else
                        cell.SetCellValue("-");
                }
                {
                    var cell = row.CreateCell(14);
                    if (service.StartDateTime.HasValue)
                        cell.SetCellValue(service.StartDateTime.Value);
                    else
                        cell.SetCellValue("-");
                }
                {
                    var cell = row.CreateCell(15);
                    if (service.Duration.HasValue)
                        cell.SetCellValue(service.Duration.Value.TotalMinutes);
                    else
                        cell.SetCellValue("-");
                }
                row.CreateCell(16).SetCellValue(service.BingingPackage?.Title ?? "-");
                row.CreateCell(17).SetCellValue(Convert.ToDouble(service.Cost));
            }
        }
    }
}