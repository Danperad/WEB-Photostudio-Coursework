using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;

namespace PhotoStudio.WebApi.Client.Services;

public class PackageService(PhotoStudioContext context, IMapper mapper) : IPackageService
{
    public async Task<IEnumerable<ServicePackageDto>> GetAllPackagesAsync()
    {
        var packages = context.ServicePackages
            .AsNoTracking()
            .OrderBy(p => p.Title);
        var res = await packages.ToListAsync();
        return mapper.Map<List<ServicePackage>, List<ServicePackageDto>>(res);
    }
}