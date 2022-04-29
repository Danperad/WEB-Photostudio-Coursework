using PhotostudioDB.Models.Services;

namespace PhotostudioDB.Models;

public class Order
{
    #region Props

    public int Id { get; internal set; }
    public int ClientId { get; internal set; }
    public DateTime DateTime { get; internal set; }
    public int StatusId { get; internal set; }
    public Client Client { get; internal set; }
    public Status Status { get; internal set; }
    public IEnumerable<ApplicationService> Services { get; internal set; }
    public int ContractId { get; set; }
    public Contract? Contract { get; set; }

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

    public Order(Client client, DateTime dateTime, IEnumerable<ApplicationService> services)
    {
        DateTime = DateTime.Now;
        Client = client;
        DateTime = dateTime;
        Status = new Status();
        Services = services;
    }

    #endregion

    #region Methods

    private bool Check()
    {
        try
        {
            var c = Client;
            var s = Status;
        }
        catch
        {
            return false;
        }

        return true;
    }

    public bool AddOrder(IList<ApplicationService> services)
    {
        return Check() && DbWorker.AddOrder(this);
    }

    #endregion
}