using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Client.Repositories.Interfaces;

namespace PhotoStudio.WebApi.Client.Repositories;

public class RentedItemRepository(PhotoStudioContext context) : IRentedItemRepository
{
    public IQueryable<RentedItem> GetItems()
    {
        return context.RentedItems;
    }
}