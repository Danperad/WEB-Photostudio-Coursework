using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.WebApi.Employee.Dto;
using PhotoStudio.WebApi.Employee.Services.Interfaces;

namespace PhotoStudio.WebApi.Employee.Services;

public class ServiceService(PhotoStudioContext context, IMapper mapper) : IServiceService
{
    public IAsyncEnumerable<SimpleServiceDto> GetServices()
    {
        var services = context.Services.AsNoTracking().OrderBy(s => s.Type).ThenBy(s => s.Title);
        return services.ProjectTo<SimpleServiceDto>(mapper.ConfigurationProvider).AsAsyncEnumerable();
    }
}