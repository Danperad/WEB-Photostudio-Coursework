using PhotoStudio.DataBase.Models;

namespace PhotoStudio.WebApi.Client.Repositories.Interfaces;

public interface IRentedItemRepository
{
    IQueryable<RentedItem> GetItems();
}