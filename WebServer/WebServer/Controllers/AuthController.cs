using Microsoft.EntityFrameworkCore;
using PhotostudioDB;
using PhotostudioDB.Models;
using RestPanda.Requests;
using RestPanda.Requests.Attributes;
using Trivial.Security;
using WebServer.Models;

namespace WebServer.Controllers;

[RequestHandlerPath("/auth")]
public class AuthController : RequestHandler
{
    private static (string, string) GenerateToken(Profile client)
    {
        var model = new JsonWebTokenPayload
        {
            Id = Guid.NewGuid().ToString("n"),
            Issuer = $"{client.Login}",
            Expiration = DateTime.Now + new TimeSpan(1, 0, 0, 0)
        };
        var refreshModel = new JsonWebTokenPayload
        {
            Id = Guid.NewGuid().ToString("n"),
            Issuer = $"{client.Login}",
            IssuedAt = DateTime.Now
        };
        var jwt = new JsonWebToken<JsonWebTokenPayload>(model, Program.Sign);
        var refreshjwt = new JsonWebToken<JsonWebTokenPayload>(refreshModel, Program.Sign);
        using var db = new ApplicationContext();
        db.Attach(client);
        db.RefreshTokens.Add(new RefreshToken(refreshjwt.ToEncodedString(), client));
        db.RefreshTokens.RemoveRange(db.RefreshTokens.Where(r => r.SignDate.AddDays(90) > DateTime.Now));
        db.SaveChanges();
        return (jwt.ToEncodedString(), refreshjwt.ToEncodedString());
    }

    private static (string, string) RefreshTokenCheck(string token)
    {
        using var db = new ApplicationContext();
        if (!db.RefreshTokens.Any(r => r.Token == token)) return ("", "");

        var rt = db.RefreshTokens.First(r => r.Token == token);
        if (rt.Profile is null) db.Profiles.Where(p => p.Id == rt.ProfileId).Load();
        if (rt.Profile is null) return ("", "");
        db.Remove(db.RefreshTokens.First(r => r.Token == token));
        db.SaveChanges();
        return GenerateToken(rt.Profile);
    }

    [Post("/signin")]
    public void LoginUser()
    {
        var body = Bind<AuthModel>();
        if (body is null || string.IsNullOrEmpty(body.Login) || string.IsNullOrEmpty(body.Password))
        {
            Send(new AnswerModel(false, null, 400));
            return;
        }

        using var db = new ApplicationContext();

        var client = db.Clients.FirstOrDefault(c =>
            c.ProfileId.HasValue && c.Profile!.Login == body.Login && c.Profile.Pass == body.Password);
        if (client is null)
        {
            Send(new AnswerModel(false, null, 101));
            return;
        }

        if (client.Profile is null) db.Profiles.Where(p => p.Id == client.ProfileId).Load();
        var tokens = GenerateToken(client.Profile!);
        Send(new AnswerModel(true, new
        {
            access_token = tokens.Item1, refresh_token = tokens.Item2, user = ClientModel.GetClientModel(client)
        }, null));
    }

    [Post("/signup")]
    public void RegisterUser()
    {
        var body = Bind<RegModel>();
        if (RegModel.Check(body))
        {
            Send(new AnswerModel(false, null, 401));
            return;
        }

        using var db = new ApplicationContext();

        if (db.Clients.Any(c => c.Phone == body!.Phone)) Send(new AnswerModel(false, null, 400));

        if (db.Clients.Any(c => c.EMail == body!.EMail)) Send(new AnswerModel(false, null, 402));

        if (db.Profiles.Any(c => c.Login == body!.Login)) Send(new AnswerModel(false, null, 40));

        var client = new Client(body!.LastName, body.FirstName, body.Phone)
        {
            MiddleName = body.MiddleName == "" ? null : body.MiddleName, EMail = body.EMail
        };

        if (!client.Check())
        {
            Send(new AnswerModel(false, null, 401));
            return;
        }

        var profile = db.Add(new Profile(client, body.Login, body.Password));

        if (db.SaveChanges() < 0)
        {
            Send(new AnswerModel(false, null, 401));
            return;
        }

        var tokens = GenerateToken(profile.Entity);
        Send(new AnswerModel(true, new
        {
            access_token = tokens.Item1, refresh_token = tokens.Item2,
            user = ClientModel.GetClientModel(profile.Entity.Client!)
        }, null));
    }

    [Get("/reauth")]
    public void ReAuthUser()
    {
        if (!Params.TryGetValue("token", out var token))
        {
            Send(new AnswerModel(false, null, 401));
            return;
        }

        var tokens = RefreshTokenCheck(token);
        if (string.IsNullOrEmpty(tokens.Item1) || string.IsNullOrEmpty(tokens.Item2))
        {
            Send(new AnswerModel(false, null, 401));
            return;
        }

        Send(new AnswerModel(true, new {access_token = tokens.Item1, refresh_token = tokens.Item2}, null));
    }
}