using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Client.Repositories.Interfaces;

namespace PhotoStudio.WebApi.Client.Repositories;

public class PackageRepository(PhotoStudioContext context) : IPackageRepository
{
    public IQueryable<ServicePackage> GetServicePackages()
    {
        return context.ServicePackages;
    }
}