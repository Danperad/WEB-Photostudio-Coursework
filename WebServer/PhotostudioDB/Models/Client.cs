namespace PhotostudioDB.Models;

public class Client : Human
{
    #region Props

    public bool IsActive { get; internal set; }
    public int? ProfileId { get; internal set; }
    public Profile? Profile { get; internal set; }
    public string? Company { get; internal set; }
    public IEnumerable<Order> Orders { get; internal set; }
    public IEnumerable<Contract> Contracts { get; internal set; }
    public string? Avatar { get; set; }

    #endregion

    #region Ctors

    internal Client()
    {
        IsActive = false;
        Orders = new List<Order>();
        Contracts = new List<Contract>();
    }

    public Client(string lastName, string firstName, string phone, string? company = null) : base(lastName, firstName,
        phone)
    {
        IsActive = true;
        Company = company;
        Orders = new List<Order>();
        Contracts = new List<Contract>();
    }

    #endregion

    public new bool Check()
    {
        return base.Check();
    }

    public bool SaveUpdate()
    {
        return DbWorker.Save();
    }
}