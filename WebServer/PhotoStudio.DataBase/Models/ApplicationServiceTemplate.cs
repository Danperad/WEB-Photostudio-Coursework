namespace PhotoStudio.DataBase.Models;

public class ApplicationServiceTemplate
{
    public int Id { get; internal set; }
    public int ServiceId { get; internal set; }
    public Service Service { get; internal set; }
    public TimeSpan? Duration { get; set; }
    public int? HallId { get; internal set; }
    public Hall? Hall { get; internal set; }
    public int? RentedItemId { get; internal set; }
    public int? Number { get; internal set; }
    public RentedItem? RentedItem { get; internal set; }
    public bool? IsFullTime { get; internal set; }
    public StatusValue StatusId { get; internal set; }
    public StatusType StatusType { get; internal set; }
    public Status Status { get; internal set; }
    public Employee? Stylist { get;internal set; }
    public int? StylistId { get;internal set; }
    public ServicePackage ServicePackage { get; internal set; }
    public int ServicePackageId { get; internal set; }

    internal ApplicationServiceTemplate()
    {
        Service = new Service();
        Status = new Status();
        ServicePackage = new ServicePackage();
    }
    
    public ApplicationService MapToApplicationService(Employee? employee = null, DateTime? startDateTime = null)
    {
        var empl = employee is null ? Stylist : null;
        return new ApplicationService
        {
            Service = Service,
            ServiceId = ServiceId,
            Employee = empl,
            EmployeeId = empl?.Id,
            IsFullTime = IsFullTime,
            StartDateTime = startDateTime,
            Hall = Hall,
            HallId = HallId,
            RentedItem = RentedItem,
            RentedItemId = RentedItemId,
            Number = Number,
            Duration = Duration,
            Status = Status,
            StatusId = StatusId,
            StatusType = StatusType,
            BingingPackage = ServicePackage,
        };
    }
}