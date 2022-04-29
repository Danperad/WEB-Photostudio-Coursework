namespace PhotostudioDB.Models.Services;

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

    #endregion

    #region Ctors

    internal ApplicationService()
    {
        Id = 0;
        Order = new Order();
        Service = new Service();
        Employee = new Employee();
        Status = new Status();
    }

    public ApplicationService(Order order, Service service, Employee employee, Status status)
    {
        Order = order;
        Service = service;
        Employee = employee;
        Status = status;
    }

    #endregion
}