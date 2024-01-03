using PhotostudioDB;
using PhotostudioDB.Models;
using WebServer.ASP.Repositories.Interfaces;

namespace WebServer.ASP.Repositories;

public class PackageRepository(ApplicationContext context) : IPackageRepository
{
    public IQueryable<ServicePackage> GetServicePackages()
    {
        return context.ServicePackages;
    }
}