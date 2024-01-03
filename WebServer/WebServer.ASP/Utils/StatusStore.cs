using PhotostudioDB.Models;

namespace WebServer.ASP.Utils;

public static class StatusStore
{
    private static IReadOnlyCollection<Status> _statuses = null!;

    public static IReadOnlyCollection<Status> Statuses
    {
        get => _statuses;
        set => _statuses ??= value;
    }
}