using Microsoft.EntityFrameworkCore;
using PhotoStudio.DataBase;
using PhotoStudio.DataBase.Models;
using PhotoStudio.WebApi.Client.Repositories.Interfaces;

namespace PhotoStudio.WebApi.Client.Repositories;

public class RefreshTokenRepository(PhotoStudioContext context) : IRefreshTokenRepository
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