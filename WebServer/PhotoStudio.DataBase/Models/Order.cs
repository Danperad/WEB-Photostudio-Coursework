namespace PhotoStudio.DataBase.Models;

public class Order
{
    #region Props

    public int Id { get; internal set; }
    public int ClientId { get; internal set; }
    public DateTime DateTime { get; internal set; }
    public StatusValue StatusId { get; internal set; }
    public StatusType StatusType { get; internal set; }
    public Client Client { get; internal set; }
    public Status Status { get; set; }
    public List<ApplicationService> Services { get; internal set; }
    public ServicePackage? ServicePackage { get; internal set; }
    public int? ServicePackageId { get; internal set; }
    public int ContractId { get; internal set; }
    public Contract? Contract { get; internal set; }
    public decimal TotalPrice { get; internal set; }

    #endregion

    #region Ctors

    internal Order()
    {
        Id = 0;
        DateTime = DateTime.MinValue;
        Client = new Client();
        Status = new Status();
        Services = new List<ApplicationService>();
    }

    public Order(Client client, DateTime dateTime, List<ApplicationService> services, Status status, ServicePackage? package = null)
    {
        DateTime = DateTime.Now;
        Client = client;
        DateTime = dateTime;
        Status = status;
        Services = services;
        ServicePackage = package;
        TotalPrice = Services.Sum(s => s.Cost);
        if (ServicePackage is not null)
        {
            TotalPrice += ServicePackage.Price;
        }
    }

    #endregion
}