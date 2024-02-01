using WebServer.ASP.Dto;

namespace WebServer.ASP.Services.Interfaces;

public interface IServiceService
{
    Task<IEnumerable<ServiceDto>> GetAllServicesAsync(int? count, int? start, int? order, int? type,
        string? search);
}