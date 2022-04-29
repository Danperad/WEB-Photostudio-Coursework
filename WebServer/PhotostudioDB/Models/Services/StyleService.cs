namespace PhotostudioDB.Models.Services;

public class StyleService : PhotoVideoService
{
    internal StyleService()
    {
        StartDateTime = DateTime.MinValue;
        Duration = 0;
        IsFullTime = false;
    }

    public StyleService(Order order, Service service, Employee employee, Status status, DateTime startDateTime,
        int duration, Address address, bool isFullTime) : base(order, service, employee, status, startDateTime,
        duration, address)
    {
        IsFullTime = isFullTime;
    }

    public bool IsFullTime { get; set; }
}