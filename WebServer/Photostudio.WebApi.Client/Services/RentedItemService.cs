using System.Diagnostics;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;

namespace PhotoStudio.WebApi.Client.Services;

public class RentedItemService(PhotoStudioContext context, IMapper mapper) : IRentedItemService
{
    public async Task<IEnumerable<RentedItemDto>> GetItemsByServiceTypeAsync(int type)
    {
        var items = await PrepareItemsByServiceType(type).ProjectTo<RentedItemDto>(mapper.ConfigurationProvider).ToListAsync();
        return items;
    }

    public async Task<IEnumerable<RentedItemDto>> GetAvailableItemsByServiceTypeAsync(DateTime start, int duration,
        int type)
    {
        var items = await PrepareAvailableItemsByServiceType(start, duration, type).ProjectTo<RentedItemDto>(mapper.ConfigurationProvider).ToListAsync();
        return items;
    }

    private IQueryable<RentedItem> PrepareItemsByServiceType(int type)
    {
        var items = context.RentedItems.AsNoTracking();
        items = type switch
        {
            1 => items.Where(i => i.Type == ItemType.Simple).OrderBy(i => i.Title),
            2 => items.Where(i => i.Type == ItemType.Cloth).OrderBy(i => i.Title),
            _ => items.Where(i => i.Type == ItemType.KidsCloth).OrderBy(i => i.Title)
        };
        return items;
    }

    private IQueryable<RentedItem> PrepareAvailableItemsByServiceType(DateTime start, int duration, int type)
    {
        var items = PrepareItemsByServiceType(type);
        Debug.Write(start);
        Debug.Write(duration);
        // items = items.GetAvailable(TimeSpan.FromMinutes(90), start, TimeSpan.FromMinutes(duration));
        return items;
    }
}