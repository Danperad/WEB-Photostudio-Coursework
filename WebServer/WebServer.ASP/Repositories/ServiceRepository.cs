using PhotostudioDB;
using PhotostudioDB.Models;
using WebServer.ASP.Repositories.Interfaces;

namespace WebServer.ASP.Repositories;

public class ServiceRepository(ApplicationContext context) : IServiceRepository
{
    public IQueryable<Service> GetServices()
    {
        return context.Services;
    }
}