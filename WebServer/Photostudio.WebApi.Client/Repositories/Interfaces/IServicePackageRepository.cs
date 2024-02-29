using PhotoStudio.DataBase.Models;

namespace PhotoStudio.WebApi.Client.Repositories.Interfaces;

public interface IServicePackageRepository
{
    IQueryable<ServicePackage> GetPackages();
}