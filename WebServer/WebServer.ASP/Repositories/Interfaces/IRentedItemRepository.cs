using PhotostudioDB.Models;

namespace WebServer.ASP.Repositories.Interfaces;

public interface IRentedItemRepository
{
    IQueryable<RentedItem> GetItems();
}