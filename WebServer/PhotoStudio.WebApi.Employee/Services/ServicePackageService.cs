using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.WebApi.Employee.Dto;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Services;

public class ServicePackageService(PhotoStudioContext context, IMapper mapper) : IServicePackageService
{
    public IAsyncEnumerable<ServicePackageWithoutPhotosDto> GetAvailableServicePackages(DateTime start)
    {
        var services = context.ServicePackages;
        return services.ProjectTo<ServicePackageWithoutPhotosDto>(mapper.ConfigurationProvider).AsAsyncEnumerable();
    }
}