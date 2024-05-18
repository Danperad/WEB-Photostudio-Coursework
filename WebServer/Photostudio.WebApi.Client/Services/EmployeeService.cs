using AutoMapper;
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
        var employees = await PrepareAvailableEmployeesByServiceId(startDate, duration, serviceId);
        return mapper.Map<List<Employee>, List<EmployeeDto>>(employees);
    }
    
    private IQueryable<Employee> PrepareEmployeesByServiceId(int serviceId)
    {
        var employees = context.Employees.AsNoTracking();
        employees = serviceId switch
        {
            2 => employees.Where(e => e.RoleId == 4),
            12 => employees.Where(e => e.RoleId == 4),
            13 => employees.Where(e => e.RoleId == 6),
            _ => employees.Where(e => e.RoleId == 2)
        };
        return employees;
    }
    
    private async Task<List<Employee>> PrepareAvailableEmployeesByServiceId(DateTime start, int duration, int serviceId)
    {
        var items = PrepareEmployeesByServiceId(serviceId);
        items = TimeUtils.GetAvailable(items, TimeSpan.FromMinutes(60), start, TimeSpan.FromMinutes(duration));
        return await items.ToListAsync();
    }
}