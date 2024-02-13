namespace PhotostudioDB.Models;

public class Service
{
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

    #region Props

    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public List<string> Photos { get; set; }
    public ServiceType Type { get; set; }
    public IEnumerable<ServicePackage> ServicePackages { get; set; }

    #endregion

    public enum ServiceType
    {
        Simple = 1,
        HallRent = 2,
        Photo = 3,
        ItemRent = 4,
        Style = 5,
    }
}