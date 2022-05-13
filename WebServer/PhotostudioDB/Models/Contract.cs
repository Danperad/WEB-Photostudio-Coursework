namespace PhotostudioDB.Models;

public class Contract
{
    #region Props

    public int Id { get; internal set; }

    public int ClientId { get; internal set; }
    public int EmployeeId { get; internal set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public Client Client { get; set; }
    public Employee Employee { get; set; }
    public int OrderId { get; internal set; }
    public Order Order { get; internal set; }
    
    #endregion

    #region Ctors
    
    internal Contract()
    {
        Id = 0;
        Client = new Client();
        Employee = new Employee();
        StartDate = DateOnly.MinValue;
        EndDate = DateOnly.MinValue;
    }
    
    public Contract(Order order, Client client, Employee employee, DateOnly startDate, DateOnly endDate)
    {
        Client = client;
        ClientId = Client.Id;
        Employee = employee;
        EmployeeId = Employee.Id;
        StartDate = startDate;
        EndDate = endDate;
    }

    #endregion

    #region Methods
    
    private bool Check()
    {
        try
        {
            var c = Client;
            var e = Employee;
            if (StartDate > EndDate) return false;
        }
        catch
        {
            return false;
        }

        return true;
    }
    
    public bool AddContract(Order order)
    {
        if (!Check()) return false;
        order.Contract = this;
        return DbWorker.Save();
    }

    #endregion
}