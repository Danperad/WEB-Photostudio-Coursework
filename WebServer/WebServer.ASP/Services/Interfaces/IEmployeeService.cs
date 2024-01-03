using WebServer.ASP.Dto;

namespace WebServer.ASP.Services.Interfaces;

public interface IEmployeeService
{
    IEnumerable<EmployeeDto> GetAvailableEmployees(DateTime startDate, int duration, int serviceId);
    Task<IEnumerable<EmployeeDto>> GetAvailableEmployeesAsync(DateTime startDate, int duration, int serviceId);
}