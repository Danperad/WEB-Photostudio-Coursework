namespace PhotostudioDB.Models;

public interface IServiced
{
    public ICollection<ApplicationService> Services { get; internal set; }
}