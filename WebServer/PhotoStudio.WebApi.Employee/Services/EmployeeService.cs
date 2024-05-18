using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.WebApi.Employee.Services.Interfaces;
using PhotoStudio.WebApi.Lib;
using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Employee.Services;

public class EmployeeService(PhotoStudioContext context, IMapper mapper) : IEmployeeService
{
    public async Task<IEnumerable<EmployeeDto>> GetAvailableEmployees(DateTime start, TimeSpan duration, int serviceId)
    {
        var employees = context.Employees.Where(e => e.BoundServices.Any(s => s.Id == serviceId))
            .Include(h => h.Services).AsQueryable();
        employees = employees.GetAvailable(TimeSpan.FromMinutes(90), start, duration);
        return await employees.ProjectTo<EmployeeDto>(mapper.ConfigurationProvider).ToListAsync();
    }
}