using PhotostudioDB.Models;
using RestPanda.Requests;
using RestPanda.Requests.Attributes;
using Trivial.Security;
using WebServer.Models;

namespace WebServer.Handlers;

[RequestHandlerPath("/auth")]
public class AuthHandler : RequestHandler
{
    private string GenerateToken(Profile client)
    {
        var model = new JsonWebTokenPayload
        {
            Id = Guid.NewGuid().ToString("n"),
            Issuer = $"{client.Login}, {client.Client!.LastName}"
        };
        var jwt = new JsonWebToken<JsonWebTokenPayload>(model, Program.Sign);

        var jwtStr = jwt.ToEncodedString();
        AuthToken.RemoveLastTokens(new TimeSpan(1, 0, 0, 0));
        return AuthToken.AddToken(jwtStr) ? jwtStr : "";
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

        Send(new AnswerModel(true, new {token = GenerateToken(client.Profile!)}, null, null));
    }

    [Post("/signon")]
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
        Send(new AnswerModel(true, new {token = GenerateToken(profile)}, null, null));
    }
}