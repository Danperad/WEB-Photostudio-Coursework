using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;
using PhotoStudio.WebApi.Lib;

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
        return mapper.Map<List<RentedItem>, List<RentedItemDto>>(items);
    }

    private IQueryable<RentedItem> PrepareItemsByServiceType(int type)
    {
        var items = context.RentedItems.AsNoTracking();
        items = type switch
        {
            5 => items.Where(i => i.Type == ItemType.Cloth).OrderBy(i => i.Title),
            6 => items.Where(i => i.Type == ItemType.Simple).OrderBy(i => i.Title),
            _ => items.Where(i => i.Type == ItemType.KidsCloth).OrderBy(i => i.Title)
        };
        return items;
    }

    private async Task<List<RentedItem>> PrepareAvailableItemsByServiceType(DateTime start, int duration, int type)
    {
        var items = PrepareItemsByServiceType(type);
        items = TimeUtils.GetAvailable(items,  TimeSpan.FromMinutes(90), start, TimeSpan.FromMinutes(duration));
        return await items.ToListAsync();
    }
}