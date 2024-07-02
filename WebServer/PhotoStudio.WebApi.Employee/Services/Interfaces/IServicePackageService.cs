using PhotoStudio.WebApi.Employee.Dto;

namespace PhotoStudio.WebApi.Employee.Services.Interfaces;

public interface IServicePackageService
{
    public IAsyncEnumerable<ServicePackageWithoutPhotosDto> GetAvailableServicePackages(DateTime start);
}