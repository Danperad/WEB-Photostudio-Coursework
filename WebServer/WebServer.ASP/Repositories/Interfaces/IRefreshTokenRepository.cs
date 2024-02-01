using PhotostudioDB.Models;

namespace WebServer.ASP.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    Task<IQueryable<RefreshToken>> GetRefreshTokensAsync();
    Task<RefreshToken> AddRefreshTokenAsync(RefreshToken token);
    Task<bool> DeleteRefreshTokenAsync(string token);
}