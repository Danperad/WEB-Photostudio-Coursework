using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;
using PhotoStudio.WebApi.Client.Utils;

namespace PhotoStudio.WebApi.Client.Services;

public class EmployeeService(PhotoStudioContext context, IMapper mapper) : IEmployeeService
{
    public async Task<IEnumerable<EmployeeDto>> GetAvailableEmployeesAsync(DateTime startDate, int duration, int serviceId)
    {
        var employees = PrepareAvailableEmployeesByServiceId(startDate, duration, serviceId);
        var res = await employees.ToListAsync();
        return mapper.Map<List<Employee>, List<EmployeeDto>>(res);
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
    
    private IQueryable<Employee> PrepareAvailableEmployeesByServiceId(DateTime start, int duration, int serviceId)
    {
        var items = PrepareEmployeesByServiceId(serviceId);
        items = TimeUtils.GetAvailable(items, 60, start, duration);
        return items;
    }
}