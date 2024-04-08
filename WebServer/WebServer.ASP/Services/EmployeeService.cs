﻿using Microsoft.EntityFrameworkCore;
using PhotostudioDB.Models;
using WebServer.ASP.Dto;
using WebServer.ASP.Repositories.Interfaces;
using WebServer.ASP.Services.Interfaces;
using WebServer.ASP.Utils;

namespace WebServer.ASP.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    public async Task<IEnumerable<EmployeeDto>> GetAvailableEmployeesAsync(DateTime startDate, int duration, int serviceId)
    {
        var employees = await PrepareAvailableEmployeesByServiceId(startDate, duration, serviceId);
        var res = employees;
        return res.Select(EmployeeDto.GetModel);
    }
    
    private IQueryable<Employee> PrepareEmployeesByServiceId(int serviceId)
    {
        var employees = employeeRepository.GetEmployees().AsNoTracking();
        employees = serviceId switch
        {
            2 => employees.Where(e => e.RoleId == 4),
            12 => employees.Where(e => e.RoleId == 4),
            13 => employees.Where(e => e.RoleId == 6),
            _ => employees.Where(e => e.RoleId == 2)
        };
        return employees;
    }
    
    private async Task<IEnumerable<Employee>> PrepareAvailableEmployeesByServiceId(DateTime start, int duration, int serviceId)
    {
        var items = await PrepareEmployeesByServiceId(serviceId).ToListAsync();
        items = TimeUtils.GetAvailable(items, 60, start, duration);
        return items;
    }
}