using Microsoft.EntityFrameworkCore;
using PhotostudioDB.Models;
using WebServer.ASP.Dto;
using WebServer.ASP.Repositories.Interfaces;
using WebServer.ASP.Services.Interfaces;

namespace WebServer.ASP.Services;

public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{
    public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync(int? count, int? start, int? order, int? type, string? search)
    {
        var services = GetPreparedServices(order, type, search);
        if (count.HasValue && start.HasValue)
            services = GetRangedServices(services, count.Value, start.Value);
        var res = await services.ToArrayAsync();
        return res.Select(ServiceDto.GetServiceModel);
    }

    private IQueryable<Service> GetPreparedServices(int? order, int? type,
        string? search)
    {
        var services = serviceRepository.GetServices().AsNoTracking();
        if (type.HasValue)
        {
            services = services.Where(s => s.Type == (Service.Status) type);
        }

        services = order switch
        {
            2 => services.OrderBy(o => o.Cost).ThenBy(o => o.Title),
            3 => services.OrderByDescending(o => o.Rating).ThenBy(o => o.Title),
            _ => services.OrderBy(o => o.Title)
        };
        if (!string.IsNullOrWhiteSpace(search))
        {
            services = services.Where(s => s.Title.ToLower().Contains(search.ToLower()));
        }

        return services;
    }

    private IQueryable<Service> GetRangedServices(IQueryable<Service> services, int count, int start)
    {
        return services.Take(new Range(start - 1, start - 1 + count));
    }
}