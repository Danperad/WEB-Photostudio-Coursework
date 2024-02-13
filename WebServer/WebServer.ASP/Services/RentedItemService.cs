using Microsoft.EntityFrameworkCore;
using PhotostudioDB.Models;
using WebServer.ASP.Dto;
using WebServer.ASP.Repositories.Interfaces;
using WebServer.ASP.Services.Interfaces;
using WebServer.ASP.Utils;

namespace WebServer.ASP.Services;

public class RentedItemService(IRentedItemRepository rentedItemRepository) : IRentedItemService
{
    public async Task<IEnumerable<RentedItemDto>> GetItemsByServiceTypeAsync(int type)
    {
        var items = PrepareItemsByServiceType(type);
        var res = await items.ToListAsync();
        return res.Select(RentedItemDto.GetModel);
    }

    public async Task<IEnumerable<RentedItemDto>> GetAvailableItemsByServiceTypeAsync(DateTime start, int duration,
        int type)
    {
        var items = PrepareAvailableItemsByServiceType(start, duration, type);
        var res = await items.ToListAsync();
        return res.Select(RentedItemDto.GetModel);
    }

    private IQueryable<RentedItem> PrepareItemsByServiceType(int type)
    {
        var items = rentedItemRepository.GetItems().AsNoTracking();
        items = type switch
        {
            5 => items.Where(i => i.IsСlothes && !i.IsKids).OrderBy(i => i.Title),
            6 => items.Where(i => !i.IsСlothes).OrderBy(i => i.Title),
            _ => items.Where(i => i.IsKids).OrderBy(i => i.Title)
        };
        return items;
    }
    
    private IQueryable<RentedItem> PrepareAvailableItemsByServiceType(DateTime start, int duration, int type)
    {
        var items = PrepareItemsByServiceType(type);
        items = TimeUtils.GetAvailable(items, 60, start, duration);
        return items;
    }
}