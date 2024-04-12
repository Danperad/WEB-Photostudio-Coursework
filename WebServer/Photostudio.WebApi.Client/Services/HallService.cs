using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;
using PhotoStudio.WebApi.Client.Utils;

namespace PhotoStudio.WebApi.Client.Services;

public class HallService(PhotoStudioContext context, IMapper mapper) : IHallService
{
    public async Task<IEnumerable<HallDto>> GetHallsAsync()
    {
        var halls = GetPreparedHalls();
        var res = await halls.ToListAsync();
        return mapper.Map<List<Hall>, List<HallDto>>(res);
    }

    public async Task<IEnumerable<HallDto>> GetAvailableHallsAsync(DateTime startDate, int duration)
    {
        var halls = GetPreparedAvailableHalls(startDate, duration);
        var res = await halls.ToListAsync();
        return mapper.Map<List<Hall>, List<HallDto>>(res);
    }

    private IQueryable<Hall> GetPreparedHalls()
    {
        var halls = context.Halls.AsQueryable();
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