using WebServer.ASP.Dto;

namespace WebServer.ASP.Services.Interfaces;

public interface IHallService
{
    IEnumerable<HallDto> GetHalls();
    Task<IEnumerable<HallDto>> GetHallsAsync();
    IEnumerable<HallDto> GetAvailableHalls(DateTime startDate, int duration);
    Task<IEnumerable<HallDto>> GetAvailableHallsAsync(DateTime startDate, int duration);
}