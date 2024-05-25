using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Employee.Services.Interfaces;
using PhotoStudio.WebApi.Employee.Dto;

namespace PhotoStudio.WebApi.Employee.Services;

public class RentedItemService(PhotoStudioContext context, IMapper mapper) : IRentedItemService
{
    private static readonly TimeSpan Const = TimeSpan.FromMinutes(90);
    public IAsyncEnumerable<RentedItemDto> GetAvailableItems(DateTime start, TimeSpan duration)
    {
        var items = context.RentedItems.Include(h => h.Services).AsQueryable();
        var periodEnd = start + duration;
        var adjustedPeriodStart = start - Const;
        var adjustedPeriodEnd = periodEnd + Const;
        items = items.Where(h =>
            !h.Services.Any(s =>
                ((s.StartDateTime - Const >= adjustedPeriodEnd && s.StartDateTime + s.Duration + Const <= adjustedPeriodEnd) ||
                 (s.StartDateTime - Const >= adjustedPeriodStart && s.StartDateTime + s.Duration + Const <= adjustedPeriodStart) ||
                 (adjustedPeriodStart >= s.StartDateTime - Const && adjustedPeriodEnd <= s.StartDateTime + s.Duration + Const)) &&
                s.StatusId != StatusValue.Canceled));
        return items.ProjectTo<RentedItemDto>(mapper.ConfigurationProvider).AsAsyncEnumerable();
    }
}