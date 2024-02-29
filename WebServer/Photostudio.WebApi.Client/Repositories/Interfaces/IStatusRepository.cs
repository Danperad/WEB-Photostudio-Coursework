using PhotoStudio.DataBase.Models;

namespace PhotoStudio.WebApi.Client.Repositories.Interfaces;

public interface IStatusRepository
{
    IQueryable<Status> GetAllStatuses();
    Task<Status> GetStatusById(int id);
}