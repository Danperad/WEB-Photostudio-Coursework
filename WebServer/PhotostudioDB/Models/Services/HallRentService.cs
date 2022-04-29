namespace PhotostudioDB.Models.Services;

public class HallRentService : TimedService
{
    #region Props

    public int HallId { get; internal set; }
    public Hall Hall { get; set; }

    #endregion

    #region Ctors

    internal HallRentService()
    {
        Hall = new Hall();
    }

    public HallRentService(Order order, Service service, Employee employee, Status status, DateTime startDateTime,
        int duration, Hall hall)
        : base(order, service, employee, status, startDateTime, duration)
    {
        Hall = hall;
    }

    #endregion
}