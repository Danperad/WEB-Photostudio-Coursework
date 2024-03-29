﻿using Microsoft.EntityFrameworkCore;
using PhotostudioDB;
using PhotostudioDB.Models;
using WebServer.ASP.Repositories.Interfaces;

namespace WebServer.ASP.Repositories;

public class EmployeeRepository(ApplicationContext context) : IEmployeeRepository
{
    public IQueryable<Employee> GetEmployees()
    {
        return context.Employees;
    }

    public Task<Employee> GetEmployeeWithRole(int roleId)
    {
        return context.Employees.OrderBy(e => e.Services.Count).FirstAsync(e => e.RoleId == roleId);
    }
}