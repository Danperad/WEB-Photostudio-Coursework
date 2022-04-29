using PhotostudioDB;
using RestPanda;
using Trivial.Security;

namespace WebServer;

internal class Program
{
    internal static HashSignatureProvider Sign { get; private set; }

    private static void Main(string[] args)
    {
        if (!DbWorker.IsLoad) return;
        Sign = HashSignatureProvider.CreateHS256("oVKJYivecvudMHCELtNHDmER7Z3ASeXZ6D14vCnXk8zzcFlqemB5S8NMeNwqThmT");
        var config = new PandaConfig();
        config.AddHeader("access-control-allow-origin", "*");
        config.AddHeader("access-control-allow-headers", "*");
        var server = new PandaServer(config, new Uri("http://localhost:8888/"));
        server.Start();
        Console.WriteLine("Server started");
        Console.Read();
        server.Stop();
    }
}