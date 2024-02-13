namespace PhotostudioDB.Models;

public class ApplicationService
{
    #region Props

    public int Id { get; internal set; }
    public int OrderId { get; internal set; }
    public int ServiceId { get; internal set; }
    public int? EmployeeId { get; internal set; }
    public int StatusId { get; internal set; }
    public Order Order { get; internal set; }
    public Service Service { get; internal set; }
    public Employee? Employee { get; set; }
    public Status Status { get; set; }
    public DateTime? StartDateTime { get; set; }
    public int? Duration { get; set; }
    public int? HallId { get; internal set; }
    public Hall? Hall { get; set; }
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

    /// <summary>
    /// Simple service ctor
    /// </summary>
    /// <param name="service"></param>
    /// <param name="status"></param>
    public ApplicationService(Service service, Status status)
    {
        Service = service;
        Status = status;
        Order = new Order();
    }

    /// <summary>
    /// Timed service ctor
    /// </summary>
    /// <param name="service"></param>
    /// <param name="startDateTime"></param>
    /// <param name="duration"></param>
    /// <param name="status"></param>
    private ApplicationService(Service service, DateTime startDateTime, int duration, Status status)
        : this(service, status)
    {
        StartDateTime = startDateTime;
        Duration = duration;
    }

    /// <summary>
    /// HallRent service ctor
    /// </summary>
    /// <param name="service"></param>
    /// <param name="startDateTime"></param>
    /// <param name="duration"></param>
    /// <param name="hall"></param>
    /// <param name="status"></param>
    public ApplicationService(Service service, DateTime startDateTime, int duration,
        Hall hall, Status status) : this(service, startDateTime, duration, status)
    {
        Hall = hall;
    }

    /// <summary>
    /// Photo service ctor
    /// </summary>
    /// <param name="service"></param>
    /// <param name="employee"></param>
    /// <param name="startDateTime"></param>
    /// <param name="duration"></param>
    /// <param name="hall"></param>
    /// <param name="status"></param>
    public ApplicationService(Service service, Employee employee, DateTime startDateTime, int duration, Hall hall,
        Status status)
        : this(service, startDateTime, duration, hall, status)
    {
        Employee = employee;
    }

    /// <summary>
    /// ItemRent service ctor
    /// </summary>
    /// <param name="service"></param>
    /// <param name="startDateTime"></param>
    /// <param name="duration"></param>
    /// <param name="number"></param>
    /// <param name="rentedItem"></param>
    /// <param name="status"></param>
    public ApplicationService(Service service, DateTime startDateTime, int duration,
        int number, RentedItem rentedItem, Status status) : this(service, startDateTime, duration, status)
    {
        Number = number;
        RentedItem = rentedItem;
    }

    /// <summary>
    /// Style service ctor
    /// </summary>
    /// <param name="service"></param>
    /// <param name="employee"></param>
    /// <param name="startDateTime"></param>
    /// <param name="duration"></param>
    /// <param name="hall"></param>
    /// <param name="isFullTime"></param>
    /// <param name="status"></param>
    public ApplicationService(Service service, Employee employee, DateTime startDateTime, int duration,
        Hall hall, bool isFullTime, Status status) : this(service, employee, startDateTime, duration, hall, status)
    {
        IsFullTime = isFullTime;
    }

    #endregion
}