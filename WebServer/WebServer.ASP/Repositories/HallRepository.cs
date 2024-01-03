using PhotostudioDB;
using PhotostudioDB.Models;
using WebServer.ASP.Repositories.Interfaces;

namespace WebServer.ASP.Repositories;

public class HallRepository(ApplicationContext context) : IHallRepository
{
    public IQueryable<Hall> GetHalls()
    {
        return context.Halls;
    }
}