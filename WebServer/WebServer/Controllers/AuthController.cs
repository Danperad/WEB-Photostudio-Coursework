using PhotostudioDB.Models;
using RestPanda.Requests;
using RestPanda.Requests.Attributes;
using Trivial.Security;
using WebServer.Models;

namespace WebServer.Controllers;

[RequestHandlerPath("/auth")]
public class AuthController : RequestHandler
{
    private (string, string) GenerateToken(Profile client)
    {
        var model = new JsonWebTokenPayload
        {
            Id = Guid.NewGuid().ToString("n"),
            Issuer = $"{client.Login}",
            Expiration = DateTime.Now + new TimeSpan(1, 0, 0)
        };
        var refreshModel = new JsonWebTokenPayload
        {
            Id = Guid.NewGuid().ToString("n"),
            Issuer = $"{client.Login}",
            IssuedAt = DateTime.Now
        };
        var jwt = new JsonWebToken<JsonWebTokenPayload>(model, Program.Sign);
        var refreshjwt = new JsonWebToken<JsonWebTokenPayload>(refreshModel, Program.Sign);
        RefreshToken.AddToken(refreshjwt.ToEncodedString(), client);
        return (jwt.ToEncodedString(), refreshjwt.ToEncodedString());
    }

    private (string, string) RefreshTokenCheck(string token)
    {
        var client = RefreshToken.ContainsToken(token);
        return client is null ? ("", "") : GenerateToken(client);
    }

    [Post("/signin")]
    public void LoginUser()
    {
        var body = Bind<AuthModel>();
        if (body is null || string.IsNullOrEmpty(body.login) || string.IsNullOrEmpty(body.password))
        {
            Send(new AnswerModel(false, null, 400, "incorrect request"));
            return;
        }

        var client = Profile.GetClientAuth(body.login, body.password);
        if (client is null)
        {
            Send(new AnswerModel(false, null, 401, "incorrect request body"));
            return;
        }

        var tokens = GenerateToken(client.Profile!);
        Send(new AnswerModel(true, new
        {
            access_token = tokens.Item1, refresh_toekn = tokens.Item2, user = new ClientModel(client.Id,
                client.LastName, client.FirstName, client.MiddleName, client.EMail!, client.Phone,
                client.Profile!.Login, client.Company, client.Avatar)
        }, null, null));
    }

    [Post("/signup")]
    public void RegisterUser()
    {
        var body = Bind<RegModel>();
        if (RegModel.Check(body))
        {
            Send(new AnswerModel(false, null, 401, "incorrect request"));
            return;
        }

        var profile = new Profile(new Client(body!.lastname, body.firstname, body.phone)
        {
            MiddleName = body.middlename == "" ? null : body.middlename, EMail = body.email == "" ? null : body.email,
        }, body.login, body.password);
        if (!profile.RegClient())
        {
            Send(new AnswerModel(false, null, 401, "incorrect request"));
            return;
        }

        var tokens = GenerateToken(profile);
        Send(new AnswerModel(true, new
        {
            access_token = tokens.Item1, refresh_token = tokens.Item2, user = new ClientModel(profile.Client!.Id,
                profile.Client.LastName, profile.Client.FirstName, profile.Client.MiddleName, profile.Client.EMail!,
                profile.Client.Phone,
                profile.Login, profile.Client.Company, profile.Client.Avatar)
        }, null, null));
    }

    [Get("/reauth")]
    public void ReAuthUser()
    {
        if (!Params.TryGetValue("token", out var token))
        {
            Send(new AnswerModel(false, null, 401, "incorrect request"));
            return;
        }

        var tokens = RefreshTokenCheck(token);
        if (string.IsNullOrEmpty(tokens.Item1) || string.IsNullOrEmpty(tokens.Item2))
        {
            Send(new AnswerModel(false, null, 401, "incorrect request"));
            return;
        }

        Send(new AnswerModel(true, new {access_token = tokens.Item1, refresh_token = tokens.Item2}, null, null));
    }
}