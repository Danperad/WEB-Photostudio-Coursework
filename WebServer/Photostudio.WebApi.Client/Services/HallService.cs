using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;
using PhotoStudio.WebApi.Lib;

namespace PhotoStudio.WebApi.Client.Services;

public class HallService(PhotoStudioContext context, IMapper mapper) : IHallService
{
    public async Task<IEnumerable<HallDto>> GetHallsAsync()
    {
        var halls = await GetPreparedHalls().ProjectTo<HallDto>(mapper.ConfigurationProvider).ToListAsync();
        return halls;
    }

    public async Task<IEnumerable<HallDto>> GetAvailableHallsAsync(DateTime startDate, int duration)
    {
        var halls = await GetPreparedAvailableHalls(startDate, duration)
            .ProjectTo<HallDto>(mapper.ConfigurationProvider).ToListAsync();
        return halls;
    }

    private IQueryable<Hall> GetPreparedAvailableHalls(DateTime startDate, int duration)
    {
        var halls = GetPreparedHalls();
        halls = halls.GetAvailable(TimeSpan.FromMinutes(90), startDate, TimeSpan.FromMinutes(duration));
        return halls;
    }

    private IQueryable<Hall> GetPreparedHalls()
    {
        var halls = context.Halls.Include(h => h.Address);
        return halls;
    }

}