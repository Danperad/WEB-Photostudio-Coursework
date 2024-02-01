using WebServer.ASP.Dto;

namespace WebServer.ASP.Services.Interfaces;

public interface IPackageService
{
    Task<IEnumerable<ServicePackageDto>> GetAllPackagesAsync();
}