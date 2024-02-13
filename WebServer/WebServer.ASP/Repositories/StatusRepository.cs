using Microsoft.EntityFrameworkCore;
using PhotostudioDB;
using PhotostudioDB.Models;
using WebServer.ASP.Repositories.Interfaces;

namespace WebServer.ASP.Repositories;

public class StatusRepository(ApplicationContext context) : IStatusRepository
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