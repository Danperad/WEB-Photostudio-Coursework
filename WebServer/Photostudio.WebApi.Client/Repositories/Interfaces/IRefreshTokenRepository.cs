using PhotoStudio.DataBase.Models;

namespace PhotoStudio.WebApi.Client.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    Task<IQueryable<RefreshToken>> GetRefreshTokensAsync();
    Task<RefreshToken> AddRefreshTokenAsync(RefreshToken token);
    Task<bool> DeleteRefreshTokenAsync(string token);
}