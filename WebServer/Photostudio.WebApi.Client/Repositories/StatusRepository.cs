using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Client.Repositories.Interfaces;

namespace PhotoStudio.WebApi.Client.Repositories;

public class StatusRepository(PhotoStudioContext context) : IStatusRepository
{
    public IQueryable<Status> GetAllStatuses()
    {
        return context.Statuses;
    }

    public Task<Status> GetStatusById(int id)
    {
        return context.Statuses.FirstAsync(s => s.Id == id);
    }
}