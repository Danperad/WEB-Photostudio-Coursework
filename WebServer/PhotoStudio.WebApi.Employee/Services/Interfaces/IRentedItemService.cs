using PhotoStudio.WebApi.Employee.Dto;

namespace PhotoStudio.WebApi.Employee.Services.Interfaces;

public interface IRentedItemService
{
    IAsyncEnumerable<RentedItemDto> GetAvailableItems(DateTime start, TimeSpan duration);
}