using PhotostudioDB.Models;

namespace WebServer.ASP.Repositories.Interfaces;

public interface IStatusRepository
{
    IQueryable<Status> GetAllStatuses();
    Task<Status> GetStatusById(int id);
}