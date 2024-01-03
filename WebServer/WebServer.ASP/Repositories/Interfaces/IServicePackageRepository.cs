using PhotostudioDB.Models;

namespace WebServer.ASP.Repositories.Interfaces;

public interface IServicePackageRepository
{
    IQueryable<ServicePackage> GetPackages();
}