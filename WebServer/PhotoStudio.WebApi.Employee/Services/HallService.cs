using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.WebApi.Employee.Services.Interfaces;
using PhotoStudio.WebApi.Lib;
using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Employee.Services;

public class HallService(PhotoStudioContext context, IMapper mapper) : IHallService
{
    public IAsyncEnumerable<HallDto> GetAvailableHalls(DateTime start, TimeSpan duration)
    {
        var halls = context.Halls.Include(h => h.Services).AsQueryable();
        halls = halls.GetAvailable(TimeSpan.FromMinutes(90), start, duration);
        return halls.ProjectTo<HallDto>(mapper.ConfigurationProvider).AsAsyncEnumerable();
    }
}