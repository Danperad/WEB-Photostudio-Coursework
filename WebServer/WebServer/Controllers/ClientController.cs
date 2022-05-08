using PhotostudioDB.Models;
using RestPanda.Requests;
using RestPanda.Requests.Attributes;
using WebServer.Models;

namespace WebServer.Controllers;

[RequestHandlerPath("/client")]
public class ClientController : RequestHandler
{
    [Post("/avatar")]
    public void AddAvatar()
    {
        if (!Headers.TryGetValue("Access-Token", out var token) || !TokenWorker.CheckToken(token))
        {
            Send(new AnswerModel(false, null, 400, "incorrect request"));
            return;
        }

        var client = TokenWorker.GetClientByToken(token);
        if (client is null)
        {
            Send(new AnswerModel(false, null, 400, "incorrect request"));
            return;
        }

        var body = Bind<AvatarModel>();
        if (body is null || string.IsNullOrEmpty(body.avatar))
        {
            Send(new AnswerModel(false, null, 400, "incorrect request"));
            return;
        }

        client.Avatar = body.avatar;
        if (!client.SaveUpdate())
        {
            Send(new AnswerModel(false, null, 400, "incorrect request"));
            return;
        }

        Send(new AnswerModel(true, new
        {
            user = new ClientModel(client.Id, client.LastName, client.FirstName, client.MiddleName, client.EMail!,
                client.Phone, client.Profile!.Login, client.Company, client.Avatar)
        }, null, null));
    }
}