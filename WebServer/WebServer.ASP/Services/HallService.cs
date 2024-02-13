using Microsoft.EntityFrameworkCore;
using PhotostudioDB.Models;
using WebServer.ASP.Dto;
using WebServer.ASP.Repositories.Interfaces;
using WebServer.ASP.Services.Interfaces;
using WebServer.ASP.Utils;

namespace WebServer.ASP.Services;

public class HallService(IHallRepository hallRepository) : IHallService
{
    public async Task<IEnumerable<HallDto>> GetHallsAsync()
    {
        var halls = GetPreparedHalls();
        var res = await halls.ToListAsync();
        return res.Select(HallDto.GetHallModel);
    }

    public async Task<IEnumerable<HallDto>> GetAvailableHallsAsync(DateTime startDate, int duration)
    {
        var halls = GetPreparedAvailableHalls(startDate, duration);
        var res = await halls.ToListAsync();
        return res.Select(HallDto.GetHallModel);
    }

    private IQueryable<Hall> GetPreparedHalls()
    {
        var halls = hallRepository.GetHalls();
        halls = halls.Include(h => h.Address);
        return halls;
    }

    private IQueryable<Hall> GetPreparedAvailableHalls(DateTime startDate, int duration)
    {
        var halls = GetPreparedHalls();
        halls = TimeUtils.GetAvailable(halls, 90, startDate, duration);
        return halls;
    }
}