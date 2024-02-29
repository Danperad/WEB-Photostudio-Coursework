using PhotoStudio.DataBase.Models;

namespace PhotoStudio.WebApi.Client.Repositories.Interfaces;

public interface IHallRepository
{
    IQueryable<Hall> GetHalls();
}