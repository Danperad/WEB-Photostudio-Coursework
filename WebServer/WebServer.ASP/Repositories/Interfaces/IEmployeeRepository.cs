using PhotostudioDB.Models;

namespace WebServer.ASP.Repositories.Interfaces;

public interface IEmployeeRepository
{
    IQueryable<Employee> GetEmployees();
    Task<Employee> GetEmployeeWithRole(int roleId);
}