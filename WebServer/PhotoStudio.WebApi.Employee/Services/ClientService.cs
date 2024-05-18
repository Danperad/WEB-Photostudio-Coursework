using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Employee.Dto;
using PhotoStudio.WebApi.Employee.Services.Interfaces;
using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Employee.Services;

public class ClientService(PhotoStudioContext context, IRabbitMqService rabbitMqService, IMapper mapper) : IClientService
{
    public async Task<IEnumerable<ClientDto>> GetClients(string? search, int count, int start)
    {
        IQueryable<Client> clients = context.Clients.AsNoTracking().OrderBy(c => c.LastName).ThenBy(c => c.FirstName);
        if (!string.IsNullOrWhiteSpace(search))
        {
            var pattern = $"%{search}%";
            clients = clients.Where(c =>
                EF.Functions.ILike(c.LastName, pattern) || EF.Functions.ILike(c.FirstName, pattern) ||
                (c.MiddleName != null && EF.Functions.ILike(c.MiddleName, pattern)));
        }

        clients = clients.Skip(start).Take(count);
        var res = await clients.ProjectTo<ClientDto>(mapper.ConfigurationProvider).ToListAsync();
        return res;
    }

    public async Task<ClientDto> AddNewClient(NewClientDto newClient)
    {
        var client = new Client(newClient.LastName, newClient.FirstName, newClient.Phone)
        {
            MiddleName = newClient.MiddleName,
            EMail = newClient.EMail
        };
        var clients = await context.Clients.AsNoTracking().Where(c => c.Phone == client.Phone || c.EMail == client.EMail).ToArrayAsync();
        if (clients.Any(c => c.Phone == client.Phone))
        {
            throw new NotImplementedException("101");
        }
        if (clients.Any(c => c.EMail == client.EMail))
        {
            throw new NotImplementedException("102");
        }

        var addedClient = context.Clients.Add(client);
        await context.SaveChangesAsync();
        rabbitMqService.SendMessage("New_Client");
        return mapper.Map<Client, ClientDto>(addedClient.Entity);
    }
}