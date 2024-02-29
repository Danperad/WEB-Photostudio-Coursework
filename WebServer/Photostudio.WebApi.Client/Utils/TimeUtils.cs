using PhotoStudio.DataBase.Models;

namespace PhotoStudio.WebApi.Client.Utils;

internal static class TimeUtils
{
    private static Func<int, DateTime, int, DateTime, int, bool> GetTimed =>
        (@const, time0, duration0, time1, duration1) =>
        {
            var timeConst = new TimeSpan(0, @const, 0);
            var endTime0 = time0 + new TimeSpan(0, duration0, 0) + timeConst;
            var endTime1 = time1 + new TimeSpan(0, duration1, 0) + timeConst;
            return !((time0 > time1 && time0 < endTime1) || (endTime0 > time1 && endTime0 < endTime1));
        };

    internal static IQueryable<T> GetAvailable<T>(IQueryable<T> serviceds, int @const, DateTime startDate, int duration)
        where T : IServiced
    {
        return serviceds.Where(h =>
            !h.Services.Any(a => GetTimed(@const, startDate, duration, a.StartDateTime!.Value, a.Duration!.Value)));
    }
}