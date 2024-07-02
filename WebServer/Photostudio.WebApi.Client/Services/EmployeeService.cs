using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;
using PhotoStudio.WebApi.Lib;

namespace PhotoStudio.WebApi.Client.Services;

public class EmployeeService(PhotoStudioContext context, IMapper mapper) : IEmployeeService
{
    public async Task<IEnumerable<EmployeeDto>> GetAvailableEmployeesAsync(DateTime startDate, int duration, int serviceId)
    {
        var employees = PrepareAvailableEmployeesByServiceId(startDate, duration, serviceId).ProjectTo<EmployeeDto>(mapper.ConfigurationProvider);
        return await employees.ToListAsync();
    }
    
    private IQueryable<Employee> PrepareEmployeesByServiceId(int serviceId)
    {
        var employees = context.Employees.AsNoTracking();
        employees = employees.Where(e => e.BoundServices.Any(s => s.Id == serviceId));
        return employees;
    }
    
    private IQueryable<Employee> PrepareAvailableEmployeesByServiceId(DateTime start, int duration, int serviceId)
    {
        var items = PrepareEmployeesByServiceId(serviceId);
        items = items.GetAvailable(TimeSpan.FromMinutes(60), start, TimeSpan.FromMinutes(duration));
        return items;
    }
}