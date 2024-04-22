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
        var halls = await GetPreparedHalls();
        return mapper.Map<List<Hall>, List<HallDto>>(halls);
    }

    public async Task<IEnumerable<HallDto>> GetAvailableHallsAsync(DateTime startDate, int duration)
    {
        var halls = await GetPreparedAvailableHalls(startDate, duration);
        return mapper.Map<List<Hall>, List<HallDto>>(halls);
    }

    private async Task<List<Hall>> GetPreparedHalls()
    {
        var halls = context.Halls.Include(h => h.Address);
        return await halls.ToListAsync();
    }

    private async Task<List<Hall>> GetPreparedAvailableHalls(DateTime startDate, int duration)
    {
        var halls = await GetPreparedHalls();
        halls = TimeUtils.GetAvailable(halls, 90, startDate, duration);
        return halls;
    }
}