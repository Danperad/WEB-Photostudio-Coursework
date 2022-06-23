using RestPanda;
using Trivial.Security;

namespace WebServer;

internal static class Program
{
    internal static HashSignatureProvider Sign { get; private set; } = null!;

    internal static Func<int, DateTime, int, DateTime, int, bool> GetTimed =>
        (@const, time0, duration0, time1, duration1) =>
        {
            var timeConst = new TimeSpan(0, @const, 0);
            var endTime0 = time0 + new TimeSpan(0, duration0, 0) + timeConst;
            var endTime1 = time1 + new TimeSpan(0, duration1, 0) + timeConst;
            return !((time0 > time1 && time0 < endTime1) || (endTime0 > time1 && endTime0 < endTime1));
        };

    private static void Main(string[] args)
    {
        Sign = HashSignatureProvider.CreateHS256("oVKJYivecvudMHCELtNHDmER7Z3ASeXZ6D14vCnXk8zzcFlqemB5S8NMeNwqThmT");
        var config = new PandaConfig();
        config.AddHeader("access-control-allow-origin", "http://localhost:3000");
        config.AddHeader("access-control-allow-headers", "*");
        var server = new PandaServer(config, new Uri("http://localhost:8888"));
        server.Start();
        Console.WriteLine("Server started");
        Console.Read();
        server.Stop();
        // DbWorker.UnLoad();
    }
}