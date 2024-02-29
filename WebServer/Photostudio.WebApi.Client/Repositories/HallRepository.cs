using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Client.Repositories.Interfaces;

namespace PhotoStudio.WebApi.Client.Repositories;

public class HallRepository(PhotoStudioContext context) : IHallRepository
{
    public IQueryable<Hall> GetHalls()
    {
        return context.Halls;
    }
}