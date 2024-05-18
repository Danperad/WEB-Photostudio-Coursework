using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Employee.Services.Interfaces;
using PhotoStudio.WebApi.Employee.Dto;
using PhotoStudio.WebApi.Lib;

namespace PhotoStudio.WebApi.Employee.Services;

public class RentedItemService(PhotoStudioContext context, IMapper mapper) : IRentedItemService
{
    public async Task<IEnumerable<RentedItemDto>> GetAvailableItems(DateTime start, TimeSpan duration)
    {
        var items = context.RentedItems.Include(h => h.Services).AsQueryable();
        items = items.GetAvailable(TimeSpan.FromMinutes(90), start, duration);
        return await items.ProjectTo<RentedItemDto>(mapper.ConfigurationProvider).ToListAsync();
    }
}