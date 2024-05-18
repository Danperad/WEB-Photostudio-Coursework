using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Employee.Services.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAvailableEmployees(DateTime start, TimeSpan duration, int serviceId);
}