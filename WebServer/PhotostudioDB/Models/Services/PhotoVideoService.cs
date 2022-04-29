namespace PhotostudioDB.Models.Services;

public class PhotoVideoService : TimedService
{
    internal PhotoVideoService()
    {
        AddressId = 0;
    }

    public PhotoVideoService(Order order, Service service, Employee employee, Status status, DateTime startDateTime,
        int duration, Address address) : base(order, service, employee, status, startDateTime, duration)
    {
        Address = address;
    }

    #region Props

    public int AddressId { get; internal set; }


    public Address Address { get; set; }

    #endregion
}