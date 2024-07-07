using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Employee.Services.Interfaces;
using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Employee.Services;

public class ServiceService(PhotoStudioContext context, IMapper mapper) : IServiceService
{
    public IAsyncEnumerable<SimpleServiceDto> GetServices()
    {
        var services = context.Services.AsNoTracking().OrderBy(o =>
            o.Type == Service.ServiceType.Photo ? 0 :
            o.Type == Service.ServiceType.Style ? 1 :
            o.Type == Service.ServiceType.ItemRent ? 2 :
            o.Type == Service.ServiceType.HallRent ? 3 : 4).ThenBy(o => o.Title).ThenBy(s => s.Title);
        return services.ProjectTo<SimpleServiceDto>(mapper.ConfigurationProvider).AsAsyncEnumerable();
    }
}