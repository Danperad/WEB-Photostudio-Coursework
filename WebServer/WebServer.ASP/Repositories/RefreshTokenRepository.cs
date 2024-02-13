using Microsoft.EntityFrameworkCore;
using PhotostudioDB;
using PhotostudioDB.Models;
using WebServer.ASP.Repositories.Interfaces;

namespace WebServer.ASP.Repositories;

public class RefreshTokenRepository(ApplicationContext context) : IRefreshTokenRepository
{
    public async Task<IQueryable<RefreshToken>> GetRefreshTokensAsync()
    {
        await context.RefreshTokens.Where(t => t.EndDate.CompareTo(DateTime.Now) == -1)
            .ExecuteDeleteAsync();
        return context.RefreshTokens;
    }

    public async Task<RefreshToken> AddRefreshTokenAsync(RefreshToken token)
    {
        await context.RefreshTokens.Where(t => t.EndDate.CompareTo(DateTime.Now) == -1)
            .ExecuteDeleteAsync();
        var newToken = await context.RefreshTokens.AddAsync(token);
        await context.SaveChangesAsync();
        return newToken.Entity;
    }

    public async Task<bool> DeleteRefreshTokenAsync(string token)
    {
        var count = await context.RefreshTokens.Where(t => t.Token == token).ExecuteDeleteAsync();
        return count == 1;
    }
}