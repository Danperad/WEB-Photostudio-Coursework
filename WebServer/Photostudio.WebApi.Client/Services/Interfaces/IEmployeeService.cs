using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Client.Services.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAvailableEmployeesAsync(DateTime startDate, int duration, int serviceId);
}