using RestPanda.Requests;
using RestPanda.Requests.Attributes;
using WebServer.Models;

namespace WebServer.Controllers;

[RequestHandlerPath("/client")]
public class ClientHandler : RequestHandler
{
    [Get("/get")]
    public void GetClient()
    {
        if (!Headers.TryGetValue("Access-Token", out var token) || !TokenWorker.CheckToken(token))
        {
            Send(new AnswerModel(false, null, 405, "don't auth"));
            return;
        }

        var client = TokenWorker.GetClientByToken(token);
        if (client is null)
        {
            Send(new AnswerModel(false, null, 406, "client not found"));
            return;
        }

        Send(new AnswerModel(true,
            new ClientModel(client.Id, client.LastName, client.FirstName, client.MiddleName, client.EMail!, client.Phone,
                client.Profile!.Login, client.Company), null, null));
    }
}