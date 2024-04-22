using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;
using PhotoStudio.WebApi.Client.Utils;

namespace PhotoStudio.WebApi.Client.Services;

public class RentedItemService(PhotoStudioContext context, IMapper mapper) : IRentedItemService
{
    public async Task<IEnumerable<RentedItemDto>> GetItemsByServiceTypeAsync(int type)
    {
        var items = await PrepareItemsByServiceType(type).ToListAsync();
        return mapper.Map<List<RentedItem>, List<RentedItemDto>>(items);
    }

    public async Task<IEnumerable<RentedItemDto>> GetAvailableItemsByServiceTypeAsync(DateTime start, int duration,
        int type)
    {
        var items = await PrepareAvailableItemsByServiceType(start, duration, type);
        return items.Select(RentedItemDto.GetModel);
    }

    private IQueryable<RentedItem> PrepareItemsByServiceType(int type)
    {
        var items = context.RentedItems.AsNoTracking();
        items = type switch
        {
            5 => items.Where(i => i.IsСlothes && !i.IsKids).OrderBy(i => i.Title),
            6 => items.Where(i => !i.IsСlothes).OrderBy(i => i.Title),
            _ => items.Where(i => i.IsKids).OrderBy(i => i.Title)
        };
        return items;
    }
    
    private async Task<List<RentedItem>> PrepareAvailableItemsByServiceType(DateTime start, int duration, int type)
    {
        var items = await PrepareItemsByServiceType(type).ToListAsync();
        items = TimeUtils.GetAvailable(items, 60, start, duration);
        return items;
    }
}