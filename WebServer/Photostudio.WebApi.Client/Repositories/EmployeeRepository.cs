using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Client.Repositories.Interfaces;

namespace PhotoStudio.WebApi.Client.Repositories;

public class EmployeeRepository(PhotoStudioContext context) : IEmployeeRepository
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