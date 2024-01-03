using PhotostudioDB.Models;

namespace WebServer.ASP.Repositories.Interfaces;

public interface IEmployeeRepository
{
    IQueryable<Employee> GetEmployees();
}