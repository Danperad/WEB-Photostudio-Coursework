using PhotoStudio.WebApi.Employee.Dto;
using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Employee.Services.Interfaces;

public interface IClientService
{
    Task<IEnumerable<ClientDto>> GetClients(string? search, int count, int start);
    Task<ClientDto> AddNewClient(NewClientDto newClient);
}