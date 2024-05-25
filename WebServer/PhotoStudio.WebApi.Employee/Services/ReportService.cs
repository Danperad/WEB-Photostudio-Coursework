using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PhotoStudio.DataBase;
using PhotoStudio.WebApi.Employee.Dto;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Services;

public class ReportService(PhotoStudioContext context, IMapper mapper) : IReportService
{
    public async Task GenReport(DateTime startDate, DateTime endDate, Stream output)
    {
        var startDateOnly = startDate.Date;
        var endDateOnly = endDate.Date;
        var orders = await context.Orders.Where(o => o.DateTime >= startDateOnly && o.DateTime <= endDateOnly)
            .ProjectTo<OrderReportDto>(mapper.ConfigurationProvider).ToListAsync();
        using var workbook = new XSSFWorkbook();
        var sheet = workbook.CreateSheet("Report");
        var rowNum = 0;
        var row = sheet.CreateRow(rowNum++);
        CreateHeader(row);
        for (var i = 0; i < orders.Count; i++)
        {
            var order = orders[i];
            row = sheet.CreateRow(rowNum++);
            row.CreateCell(0).SetCellValue(i + 1);
            row.CreateCell(1).SetCellValue(order.Number);
            row.CreateCell(2).SetCellValue(order.DateTime.ToLocalTime().ToString("dd-MM-yy"));
            row.CreateCell(3).SetCellValue(order.Client.LastName);
            row.CreateCell(4).SetCellValue(order.Status);
            row.CreateCell(5).SetCellValue(order.ServicePackage?.Title ?? "-");
            row.CreateCell(6).SetCellValue(Convert.ToDouble(order.TotalPrice));
            foreach (var service in order.Services)
            {
                row = sheet.CreateRow(rowNum++);
                row.CreateCell(7).SetCellValue(service.Service);
                row.CreateCell(8).SetCellValue(service.Employee.LastName);
                row.CreateCell(9).SetCellValue(service.Status);
                row.CreateCell(10).SetCellValue(service.Hall ?? "-");
                row.CreateCell(11).SetCellValue(service.Item ?? "-");
                {
                    var cell = row.CreateCell(12);
                    if (service.Number.HasValue)
                        cell.SetCellValue(service.Number.Value);
                    else
                        cell.SetCellValue("-");
                }
                {
                    var cell = row.CreateCell(13);
                    cell.SetCellValue(service.StartDateTime.HasValue
                        ? service.StartDateTime.Value.ToLocalTime().ToString("dd-MM-yy hh:mm")
                        : "-");
                }
                {
                    var cell = row.CreateCell(14);
                    if (service.Duration.HasValue)
                        cell.SetCellValue(service.Duration.Value.TotalMinutes);
                    else
                        cell.SetCellValue("-");
                }
                row.CreateCell(15).SetCellValue(service.BingingPackage?.Title ?? "-");
                row.CreateCell(16).SetCellValue(Convert.ToDouble(service.Cost));
            }
        }
        int lastColumNum = sheet.GetRow(0).LastCellNum;
        for (var i = 0; i <= lastColumNum; i++)
        {
            sheet.AutoSizeColumn(i);
            GC.Collect();
        }
        sheet.Header.Center = "This is a test sheet";
        sheet.Footer.Left = "Copyright Nissl Lab";
        sheet.Footer.Right = "created by NPOI team";
        workbook.Write(output, true);
    }

    private static void CreateHeader(IRow row)
    {
        row.CreateCell(0).SetCellValue("Номер в отчете");
        row.CreateCell(1).SetCellValue("Номер заявки");
        row.CreateCell(2).SetCellValue("Дата");
        row.CreateCell(3).SetCellValue("Клиент");
        row.CreateCell(4).SetCellValue("Статус");
        row.CreateCell(5).SetCellValue("Пакет услуг");
        row.CreateCell(6).SetCellValue("Общая стоимость (Р.)");
        row.CreateCell(7).SetCellValue("Услуга");
        row.CreateCell(8).SetCellValue("Сотрудник");
        row.CreateCell(9).SetCellValue("Статус");
        row.CreateCell(10).SetCellValue("Зал");
        row.CreateCell(11).SetCellValue("Предмет");
        row.CreateCell(12).SetCellValue("Кол-во");
        row.CreateCell(13).SetCellValue("Дата начала");
        row.CreateCell(14).SetCellValue("Длительность (мин.)");
        row.CreateCell(15).SetCellValue("Связанный пакет услуг");
        row.CreateCell(16).SetCellValue("Стоимость (Р.)");
    }
}