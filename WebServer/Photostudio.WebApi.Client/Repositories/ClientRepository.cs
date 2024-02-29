using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Client.Repositories.Interfaces;

namespace PhotoStudio.WebApi.Client.Repositories;

public class ClientRepository(PhotoStudioContext context) : IClientRepository
{
    public IQueryable<PhotoStudio.DataBase.Models.Client> GetClients()
    {
        return context.Clients;
    }

    public async Task<PhotoStudio.DataBase.Models.Client> AddClientAsync(PhotoStudio.DataBase.Models.Client client)
    {
        var clients = await context.Clients.Where(c => c.EMail == client.EMail || c.Phone == client.Phone).ToListAsync();
        if (clients.Count != 0)
        {
            throw new NotImplementedException(clients.Any(c => c.EMail == client.EMail)
                ? 402.ToString()
                : 400.ToString());
        }

        var newClient = await context.Clients.AddAsync(client);
        await context.SaveChangesAsync();
        return newClient.Entity;
    }

    public async Task<PhotoStudio.DataBase.Models.Client> UpdateClientAsync(PhotoStudio.DataBase.Models.Client client)
    {
        var contextClient = context.Entry(client);
        contextClient.State = EntityState.Modified;
        await context.SaveChangesAsync();
        return contextClient.Entity;
    }
}