using PhotoStudio.WebApi.Employee.Dto;

namespace PhotoStudio.WebApi.Employee.Services.Interfaces;

public interface IRentedItemService
{
    Task<IEnumerable<RentedItemDto>> GetAvailableItems(DateTime start, TimeSpan duration);
}