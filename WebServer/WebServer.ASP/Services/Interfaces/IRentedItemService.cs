using WebServer.ASP.Dto;

namespace WebServer.ASP.Services.Interfaces;

public interface IRentedItemService
{
    IEnumerable<RentedItemDto> GetItemsByServiceType(int type);
    Task<IEnumerable<RentedItemDto>> GetItemsByServiceTypeAsync(int type);

    IEnumerable<RentedItemDto> GetAvailableItemsByServiceType(DateTime start, int duration, int type);
    Task<IEnumerable<RentedItemDto>> GetAvailableItemsByServiceTypeAsync(DateTime start, int duration, int type);

}