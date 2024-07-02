using PhotoStudio.WebApi.Employee.Dto;
using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Employee.Services.Interfaces;

public interface IEmployeeService
{
    IAsyncEnumerable<EmployeeDto> GetAvailableEmployees(DateTime start, TimeSpan duration, int serviceId);
    IAsyncEnumerable<EmployeeWithRoleDto> GetAllEmployees();
}