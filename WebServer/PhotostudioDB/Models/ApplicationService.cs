namespace PhotostudioDB.Models;

public class ApplicationService
{
    #region Props

    public int Id { get; internal set; }
    public int OrderId { get; internal set; }
    public int ServiceId { get; internal set; }
    public int EmployeeId { get; internal set; }
    public int StatusId { get; internal set; }
    public Order Order { get; internal set; }
    public Service Service { get; internal set; }
    public Employee Employee { get; set; }
    public Status Status { get; set; }
    public DateTime? StartDateTime { get; set; }
    public int? Duration { get; set; }
    public int? HallId { get; internal set; }
    public Hall? Hall { get; set; }
    public int? AddressId { get; internal set; }
    public Address? Address { get; set; }
    public int? RentedItemId { get; internal set; }
    public int? Number { get; internal set; }
    public RentedItem? RentedItem { get; internal set; }
    public bool? IsFullTime { get; set; }

    #endregion

    #region Ctors

    internal ApplicationService()
    {
        Order = new Order();
        Service = new Service();
        Employee = new Employee();
        Status = new Status();
    }

    public ApplicationService(Service service, Employee employee)
    {
        Service = service;
        Employee = employee;
        Status = new Status();
        Order = new Order();
    }

    private ApplicationService(Service service, Employee employee, DateTime startDateTime, int duration)
        : this(service, employee)
    {
        StartDateTime = startDateTime;
        Duration = duration;
    }

    public ApplicationService(Service service, Employee employee, DateTime startDateTime, int duration,
        Hall hall) : this(service, employee, startDateTime, duration)
    {
        Hall = hall;
    }

    public ApplicationService(Service service, Employee employee, DateTime startDateTime, int duration,
        Address address) : this(service, employee, startDateTime, duration)
    {
        Address = address;
    }

    public ApplicationService(Service service, Employee employee, DateTime startDateTime, int duration,
        int number, RentedItem rentedItem) : this(service, employee, startDateTime, duration)
    {
        Number = number;
        RentedItem = rentedItem;
    }

    public ApplicationService(Service service, Employee employee, DateTime startDateTime, int duration,
        Address address, bool isFullTime) : this(service, employee, startDateTime, duration, address)
    {
        IsFullTime = isFullTime;
    }

    #endregion
}