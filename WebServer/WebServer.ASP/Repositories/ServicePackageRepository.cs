using PhotostudioDB;
using PhotostudioDB.Models;
using WebServer.ASP.Repositories.Interfaces;

namespace WebServer.ASP.Repositories;

public class ServicePackageRepository(ApplicationContext context) : IServicePackageRepository
{
    public IQueryable<ServicePackage> GetPackages()
    {
        return context.ServicePackages;
    }
}