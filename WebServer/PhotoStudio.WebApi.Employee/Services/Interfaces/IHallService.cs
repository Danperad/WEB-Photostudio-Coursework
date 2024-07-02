using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Employee.Services.Interfaces;

public interface IHallService
{
    IAsyncEnumerable<HallDto> GetAvailableHalls(DateTime start, TimeSpan duration);
}