using PhotostudioDB.Models;

namespace WebServer.ASP.Repositories.Interfaces;

public interface IHallRepository
{
    IQueryable<Hall> GetHalls();
}