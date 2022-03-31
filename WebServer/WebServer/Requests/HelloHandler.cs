using RestPanda.Requests;
using RestPanda.Requests.Attributes;

namespace WebServer.Requests;

[RequestHandler]
public class HelloHandler
{
    [Get]
    public static void HelloSay(PandaRequest request, PandaResponse response)
    {
        response.Send(new{Hello="World!"});
    }
}