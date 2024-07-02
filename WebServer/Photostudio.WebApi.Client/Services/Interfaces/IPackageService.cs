using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Client.Services.Interfaces;

public interface IPackageService
{
    Task<IEnumerable<ServicePackageDto>> GetAllPackagesAsync();
}