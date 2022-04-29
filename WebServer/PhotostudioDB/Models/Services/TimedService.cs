namespace PhotostudioDB.Models.Services;

public abstract class TimedService : ApplicationService
{
    protected TimedService()
    {
        StartDateTime = DateTime.MinValue;
        Duration = 0;
    }

    protected TimedService(Order order, Service service, Employee employee,Status status, DateTime startDateTime, int duration) :
        base(
            order, service, employee, status)
    {
        StartDateTime = startDateTime;
        Duration = duration;
    }

    public DateTime StartDateTime { get; set; }
    public int Duration { get; set; }
}