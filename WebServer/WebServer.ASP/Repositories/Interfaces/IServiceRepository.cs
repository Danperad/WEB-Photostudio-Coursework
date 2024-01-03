using PhotostudioDB.Models;

namespace WebServer.ASP.Repositories.Interfaces;

public interface IServiceRepository
{
    IQueryable<Service> GetServices();
}