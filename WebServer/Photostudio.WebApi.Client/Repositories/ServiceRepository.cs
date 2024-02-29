using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Client.Repositories.Interfaces;

namespace PhotoStudio.WebApi.Client.Repositories;

public class ServiceRepository(PhotoStudioContext context) : IServiceRepository
{
    public IQueryable<Service> GetServices()
    {
        return context.Services;
    }
}