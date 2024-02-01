using WebServer.ASP.Dto;

namespace WebServer.ASP.Services.Interfaces;

public interface IHallService
{
    Task<IEnumerable<HallDto>> GetHallsAsync();
    Task<IEnumerable<HallDto>> GetAvailableHallsAsync(DateTime startDate, int duration);
}