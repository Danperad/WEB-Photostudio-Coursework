using PhotostudioDB;
using PhotostudioDB.Models;
using WebServer.ASP.Repositories.Interfaces;

namespace WebServer.ASP.Repositories;

public class RentedItemRepository(ApplicationContext context) : IRentedItemRepository
{
    public IQueryable<RentedItem> GetItems()
    {
        return context.RentedItems;
    }
}