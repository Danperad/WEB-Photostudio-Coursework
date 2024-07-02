namespace PhotoStudio.DataBase.Models;

public interface IServiced
{
    public List<ApplicationService> Services { get; internal set; }
}