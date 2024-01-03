using PhotostudioDB.Models;

namespace WebServer.ASP.Repositories.Interfaces;

public interface IClientRepository
{
    IQueryable<Client> GetClients();
    Client AddClient(Client client);
    Task<Client> AddClientAsync(Client client);
    Client UpdateClient(Client client);
    Task<Client> UpdateClientAsync(Client client);
}