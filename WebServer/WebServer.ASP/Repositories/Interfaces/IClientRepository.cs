using PhotostudioDB.Models;

namespace WebServer.ASP.Repositories.Interfaces;

public interface IClientRepository
{
    IQueryable<Client> GetClients();
    Task<Client> AddClientAsync(Client client);
    Task<Client> UpdateClientAsync(Client client);
}