using Microsoft.EntityFrameworkCore;
using WebServer.ASP.Dto;
using WebServer.ASP.Repositories.Interfaces;
using WebServer.ASP.Services.Interfaces;

namespace WebServer.ASP.Services;

public class PackageService(IPackageRepository packageRepository) : IPackageService
{
    public IEnumerable<ServicePackageDto> GetAllPackages()
    {
        var packages = packageRepository.GetServicePackages()
            .AsNoTracking()
            .OrderBy(p => p.Title);
        var res = packages.ToArray();
        return res.Select(ServicePackageDto.GetServiceModel);
    }

    public async Task<IEnumerable<ServicePackageDto>> GetAllPackagesAsync()
    {
        var packages = packageRepository.GetServicePackages()
            .AsNoTracking()
            .OrderBy(p => p.Title);
        var res = await packages.ToArrayAsync();
        return res.Select(ServicePackageDto.GetServiceModel);
    }
}