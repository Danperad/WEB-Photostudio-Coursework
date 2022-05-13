namespace PhotostudioDB.Models;

public class Service
{
    public static IEnumerable<Service> GetServices()
    {
        return DbWorker.GetServices();
    }

    #region Props

    public int Id { get; internal set; }
    public string Title { get;internal set; }
    public string Description { get;internal set; }
    public decimal? Cost { get;internal set; }
    public List<string> Photos { get; internal set; }
    public int Type { get;internal set; }
    public IEnumerable<ServicePackage> ServicePackages { get; set; }

    #endregion

    #region Ctors

    internal Service()
    {
        Id = 0;
        Title = "";
        Description = "";
        Type = 0;
        Photos = new List<string>();
        ServicePackages = new List<ServicePackage>();
    }

    #endregion
}