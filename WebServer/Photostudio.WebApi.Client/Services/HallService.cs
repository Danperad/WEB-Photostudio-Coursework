using AutoMapper;
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
        var halls = await GetPreparedHalls().ToListAsync();
        return mapper.Map<List<Hall>, List<HallDto>>(halls);
    }

    public async Task<IEnumerable<HallDto>> GetAvailableHallsAsync(DateTime startDate, int duration)
    {
        var halls = await GetPreparedAvailableHalls(startDate, duration);
        return mapper.Map<List<Hall>, List<HallDto>>(halls);
    }

    private async Task<List<Hall>> GetPreparedAvailableHalls(DateTime startDate, int duration)
    {
        var halls = GetPreparedHalls();
        halls = TimeUtils.GetAvailable(halls, TimeSpan.FromMinutes(90), startDate, TimeSpan.FromMinutes(duration));
        return await halls.ToListAsync();
    }

    private IQueryable<Hall> GetPreparedHalls()
    {
        var halls = context.Halls.Include(h => h.Address);
        return halls;
    }

}