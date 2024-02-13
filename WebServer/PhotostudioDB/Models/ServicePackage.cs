namespace PhotostudioDB.Models;

public class ServicePackage
{
    internal ServicePackage()
    {
        Title = "";
        Description = "";
        Photograph = new Employee();
        Hall = new Hall();
        Photos = new List<string>();
        Services = new HashSet<ApplicationServiceTemplate>();
    }

    #region Props

    public int Id { get; internal set; }
    public string Title { get; internal set; }
    public string Description { get; internal set; }
    public List<string> Photos { get; internal set; }
    public ISet<ApplicationServiceTemplate> Services { get; internal set; }
    public Hall Hall { get; internal set; }
    public int HallId { get; internal set; }
    public Employee Photograph { get; internal set; }
    public int EmployeeId { get; internal set; }
    public int Duration { get; internal set; }
    public decimal Price { get; internal set; }

    #endregion
}