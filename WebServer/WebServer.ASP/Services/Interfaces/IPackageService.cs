using WebServer.ASP.Dto;

namespace WebServer.ASP.Services.Interfaces;

public interface IPackageService
{
    IEnumerable<ServicePackageDto> GetAllPackages();
    Task<IEnumerable<ServicePackageDto>> GetAllPackagesAsync();
}