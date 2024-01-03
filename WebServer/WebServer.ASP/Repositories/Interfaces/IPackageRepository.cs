using PhotostudioDB.Models;

namespace WebServer.ASP.Repositories.Interfaces;

public interface IPackageRepository
{
    IQueryable<ServicePackage> GetServicePackages();
}