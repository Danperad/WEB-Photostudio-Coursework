using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Lib.Dto;
using PhotoStudio.WebApi.Client.Repositories.Interfaces;
using PhotoStudio.WebApi.Client.Services.Interfaces;

namespace PhotoStudio.WebApi.Client.Services;

public class ServiceService(IServiceRepository serviceRepository, IMapper mapper) : IServiceService
{
    public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync(int? count, int? start, int? order, int? type, string? search)
    {
        var services = GetPreparedServices(order, type, search);
        if (count.HasValue && start.HasValue)
            services = GetRangedServices(services, count.Value, start.Value);
        var res = await services.ToListAsync();
        return mapper.Map<List<Service>, List<ServiceDto>>(res);
    }

    private IQueryable<Service> GetPreparedServices(int? order, int? type,
        string? search)
    {
        var services = serviceRepository.GetServices().AsNoTracking();
        if (type.HasValue)
        {
            services = services.Where(s => s.Type == (Service.ServiceType) type);
        }

        services = order switch
        {
            2 => services.OrderBy(o => o.Cost).ThenBy(o => o.Title),
            _ => services.OrderBy(o => o.Title)
        };
        if (!string.IsNullOrWhiteSpace(search))
        {
            services = services.Where(s => s.Title.ToLower().Contains(search.ToLower(), StringComparison.Ordinal));
        }

        return services;
    }

    private static IQueryable<Service> GetRangedServices(IQueryable<Service> services, int count, int start)
    {
        return services.Skip(start - 1).Take(count);
    }
}