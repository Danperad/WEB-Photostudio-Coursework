using Microsoft.EntityFrameworkCore;
using PhotostudioDB;
using PhotostudioDB.Models;
using WebServer.ASP.Repositories.Interfaces;

namespace WebServer.ASP.Repositories;

public class RefreshTokenRepository(ApplicationContext context) : IRefreshTokenRepository
{
    public IQueryable<RefreshToken> GetRefreshTokens()
    {
        context.RefreshTokens.Where(t => t.EndDate.CompareTo(DateTime.Now) == -1)
            .ExecuteDelete();
        return context.RefreshTokens;
    }

    public async Task<IQueryable<RefreshToken>> GetRefreshTokensAsync()
    {
        await context.RefreshTokens.Where(t => t.EndDate.CompareTo(DateTime.Now) == -1)
            .ExecuteDeleteAsync();
        return context.RefreshTokens;
    }

    public RefreshToken AddRefreshToken(RefreshToken token)
    {
        var newToken = context.RefreshTokens.Add(token);
        context.RefreshTokens.Where(t => t.EndDate.CompareTo(DateTime.Now) == -1)
            .ExecuteDelete();
        context.SaveChanges();
        return newToken.Entity;
    }

    public async Task<RefreshToken> AddRefreshTokenAsync(RefreshToken token)
    {
        var newToken = await context.RefreshTokens.AddAsync(token);
        await context.RefreshTokens.Where(t => t.EndDate.CompareTo(DateTime.Now) == -1)
            .ExecuteDeleteAsync();
        await context.SaveChangesAsync();
        return newToken.Entity;
    }

    public bool DeleteRefreshToken(string token)
    {
        var count = context.RefreshTokens.Where(t => t.Token == token).ExecuteDelete();
        return count == 1;
    }

    public async Task<bool> DeleteRefreshTokenAsync(string token)
    {
        var count = await context.RefreshTokens.Where(t => t.Token == token).ExecuteDeleteAsync();
        return count == 1;
    }
}