namespace PhotostudioDB.Models;

public class Service : ICloneable
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

    public object Clone()
    {
        return new Service
            {Cost = Cost, Description = Description, Id = Id, Photos = Photos, Title = Title, Type = Type};
    }
    /*public static IEnumerable<Service> GetServices()
    {
        return DbWorker.GetServices();
    }*/

    #region Props

    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public List<string> Photos { get; set; }
    public int Type { get; set; }
    public IEnumerable<ServicePackage> ServicePackages { get; set; }
    public double Rating { get; set; }

    #endregion
}