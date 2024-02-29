using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Client.Repositories.Interfaces;

namespace PhotoStudio.WebApi.Client.Repositories;

public class ServicePackageRepository(PhotoStudioContext context) : IServicePackageRepository
{
    public IQueryable<ServicePackage> GetPackages()
    {
        return context.ServicePackages;
    }
}