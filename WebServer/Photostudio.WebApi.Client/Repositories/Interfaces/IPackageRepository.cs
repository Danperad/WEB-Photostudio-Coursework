using PhotoStudio.DataBase.Models;

namespace PhotoStudio.WebApi.Client.Repositories.Interfaces;

public interface IPackageRepository
{
    IQueryable<ServicePackage> GetServicePackages();
}