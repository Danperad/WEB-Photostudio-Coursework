using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Client.Services.Interfaces;

public interface IHallService
{
    Task<IEnumerable<HallDto>> GetHallsAsync();
    Task<IEnumerable<HallDto>> GetAvailableHallsAsync(DateTime startDate, int duration);
}