using Microsoft.EntityFrameworkCore;
using PhotostudioDB;
using PhotostudioDB.Models;
using WebServer.ASP.Repositories.Interfaces;

namespace WebServer.ASP.Repositories;

public class ClientRepository(ApplicationContext context) : IClientRepository
{
    public IQueryable<Client> GetClients()
    {
        return context.Clients;
    }

    public async Task<Client> AddClientAsync(Client client)
    {
        var clients = await context.Clients.Where(c => c.EMail == client.EMail || c.Phone == client.Phone).ToArrayAsync();
        if (clients.Length == 0)
        {
            throw new NotImplementedException(clients.Any(c => c.EMail == client.EMail)
                ? 402.ToString()
                : 400.ToString());
        }

        var newClient = await context.Clients.AddAsync(client);
        await context.SaveChangesAsync();
        return newClient.Entity;
    }

    public async Task<Client> UpdateClientAsync(Client client)
    {
        var contextClient = context.Entry(client);
        contextClient.State = EntityState.Modified;
        await context.SaveChangesAsync();
        return contextClient.Entity;
    }
}