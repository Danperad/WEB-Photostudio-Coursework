using PhotoStudio.DataBase.Models;

namespace PhotoStudio.WebApi.Lib;

public static class TimeUtils
{
    public static IQueryable<T> GetAvailable<T>(this IQueryable<T> services, TimeSpan @const, DateTime startDate,
        TimeSpan duration)
        where T : IServiced
    {
        var periodEnd = startDate + duration;
        var adjustedPeriodStart = startDate - @const;
        var adjustedPeriodEnd = periodEnd + @const;

        return services.Where(h =>
            !h.Services.Any(a =>
                a.StartDateTime + a.Duration <= adjustedPeriodStart || a.StartDateTime >= adjustedPeriodEnd));
    }
}