using PhotoStudio.DataBase.Models;

namespace PhotoStudio.WebApi.Client.Repositories.Interfaces;

public interface IClientRepository
{
    IQueryable<PhotoStudio.DataBase.Models.Client> GetClients();
    Task<PhotoStudio.DataBase.Models.Client> AddClientAsync(PhotoStudio.DataBase.Models.Client client);
    Task<PhotoStudio.DataBase.Models.Client> UpdateClientAsync(PhotoStudio.DataBase.Models.Client client);
}