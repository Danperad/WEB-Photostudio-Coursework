using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Client.Services.Interfaces;

public interface IRentedItemService
{
    Task<IEnumerable<RentedItemDto>> GetItemsByServiceTypeAsync(int type);

    Task<IEnumerable<RentedItemDto>> GetAvailableItemsByServiceTypeAsync(DateTime start, int duration, int type);

}