namespace PhotoStudio.WebApi.Employee.Services.Interfaces;

public interface IReportService
{
    Task GenReport(DateTime startDate, DateTime endDate);
}