using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Client.Services.Interfaces;

public interface IServiceService
{
    Task<IEnumerable<ServiceDto>> GetAllServicesAsync(int? count, int? start, int? order, int? type,
        string? search);
}