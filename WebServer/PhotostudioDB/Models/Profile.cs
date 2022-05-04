namespace PhotostudioDB.Models;

public class Profile
{
    #region Props

    public int Id { get; internal set; }
    public string Login { get; set; }
    public string Pass { get; set; }
    public bool IsActive { get; set; }
    internal int? ClientId { get; set; }
    public Client? Client { get; internal set; }
    internal int? EmployeeId { get; set; }
    internal Employee? Employee { get; set; }

    #endregion

    #region Ctors

    internal Profile()
    {
        Id = 0;
        Login = "";
        Pass = "";
        IsActive = false;
    }

    public Profile(Client client, string login, string pass)
    {
        Client = client;
        Login = login;
        Pass = pass;
        IsActive = true;
    }

    #endregion

    #region Methods

    public static Client? GetClientAuth(string login, string pass)
    {
        return DbWorker.GetClientAuth(login, pass);
    }

    public static Client? GetClientByLogin(string login)
    {
        return DbWorker.GetClientLogin(login);
    }
    
    public bool RegClient()
    {
        if (Client is null) return false;
        return Client.Check() && DbWorker.RegisterClient(this);
    }
    
    #endregion
}