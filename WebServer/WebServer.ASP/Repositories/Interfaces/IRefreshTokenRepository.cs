using PhotostudioDB.Models;

namespace WebServer.ASP.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    IQueryable<RefreshToken> GetRefreshTokens();
    Task<IQueryable<RefreshToken>> GetRefreshTokensAsync();
    RefreshToken AddRefreshToken(RefreshToken token);
    Task<RefreshToken> AddRefreshTokenAsync(RefreshToken token);
    bool DeleteRefreshToken(string token);
    Task<bool> DeleteRefreshTokenAsync(string token);
}