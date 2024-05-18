namespace PhotoStudio.DataBase.Models;

public class Service
{
    #region Ctors

    public Service()
    {
        Id = 0;
        Title = "";
        Description = "";
        Type = 0;
        Photos = new List<string>();
        ServicePackages = new List<ServicePackage>();
        BoundEmployees = new List<Employee>();
    }

    #endregion

    #region Props

    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public List<string> Photos { get; internal set; }
    public ServiceType Type { get; set; }
    public List<ServicePackage> ServicePackages { get; internal set; }
    public List<Employee> BoundEmployees { get; internal set; }

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