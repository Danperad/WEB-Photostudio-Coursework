using PhotoStudio.DataBase.Models;

namespace PhotoStudio.WebApi.Client.Repositories.Interfaces;

public interface IEmployeeRepository
{
    IQueryable<Employee> GetEmployees();
    Task<Employee> GetEmployeeWithRole(int roleId);
}