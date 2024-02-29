using PhotoStudio.DataBase.Models;

namespace PhotoStudio.WebApi.Client.Repositories.Interfaces;

public interface IServiceRepository
{
    IQueryable<Service> GetServices();
}