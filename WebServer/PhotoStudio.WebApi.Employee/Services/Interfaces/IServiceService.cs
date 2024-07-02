using PhotoStudio.WebApi.Employee.Dto;
using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Employee.Services.Interfaces;

public interface IServiceService
{
    IAsyncEnumerable<SimpleServiceDto> GetServices();
}