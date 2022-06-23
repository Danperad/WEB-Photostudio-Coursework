using Microsoft.EntityFrameworkCore;
using PhotostudioDB;
using PhotostudioDB.Models;
using Trivial.Security;

namespace WebServer;

public static class TokenWorker
{
    internal static bool CheckToken(string token)
    {
        var parser = new JsonWebToken<JsonWebTokenPayload>.Parser(token);
        return parser.Verify(Program.Sign) && parser.GetPayload().Expiration > DateTime.Now;
    }

    internal static Client? GetClientByToken(string token)
    {
        var parser = new JsonWebToken<JsonWebTokenPayload>.Parser(token);
        var login = parser.GetPayload().Issuer;
        using var db = new ApplicationContext();
        var client = db.Clients.FirstOrDefault(c => c.ProfileId.HasValue && c.Profile!.Login == login);
        if (client is null) return null;
        if (client.Profile is null) db.Profiles.Where(p => p.Id == client.ProfileId).Load();
        return client;
    }
}