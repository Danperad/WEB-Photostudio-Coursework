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
        var rrr = services.Where(h =>
            !h.Services.Any(s =>
                ((s.StartDateTime - @const >= adjustedPeriodEnd && s.StartDateTime + s.Duration + @const <= adjustedPeriodEnd) ||
                 (s.StartDateTime - @const >= adjustedPeriodStart && s.StartDateTime + s.Duration + @const <= adjustedPeriodStart) ||
                 (adjustedPeriodStart >= s.StartDateTime - @const && adjustedPeriodEnd <= s.StartDateTime + s.Duration + @const)) && s.StatusId != StatusValue.Canceled));
        return rrr;
    }
}