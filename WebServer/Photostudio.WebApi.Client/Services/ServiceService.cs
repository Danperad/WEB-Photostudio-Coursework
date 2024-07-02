using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Services.Interfaces;

namespace PhotoStudio.WebApi.Client.Services;

public class ServiceService(PhotoStudioContext context, IMapper mapper) : IServiceService
{
    public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync(int? count, int? start, int? type,
        string? search)
    {
        var services = GetPreparedServices(type, search);
        if (count.HasValue && start.HasValue)
            services = GetRangedServices(services, count.Value, start.Value);
        var res = await services.ProjectTo<ServiceDto>(mapper.ConfigurationProvider).ToListAsync();
        return res;
    }

    private IQueryable<Service> GetPreparedServices( int? type, string? search)
    {
        var services = context.Services.AsNoTracking();
        if (type.HasValue)
        {
            services = services.Where(s => s.Type == (Service.ServiceType)type);
        }

        services = services.OrderBy(o =>
            o.Type == Service.ServiceType.Photo ? 0 :
            o.Type == Service.ServiceType.Style ? 1 :
            o.Type == Service.ServiceType.ItemRent ? 2 :
            o.Type == Service.ServiceType.HallRent ? 3 : 4).ThenBy(o => o.Title);
        if (!string.IsNullOrWhiteSpace(search))
        {
            services = services.Where(s => s.Title.ToLower().Contains(search.ToLower()));
        }

        return services;
    }

    private static IQueryable<Service> GetRangedServices(IQueryable<Service> services, int count, int start)
    {
        return services.Skip(start - 1).Take(count);
    }
}