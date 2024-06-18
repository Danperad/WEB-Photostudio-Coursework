using System.ComponentModel.DataAnnotations;

namespace PhotoStudio.DataBase.Models;

public class Employee : Human, IServiced
{
    #region Ctors

    internal Employee()
    {
        Passport = "";
        Password = "";
        Date = DateOnly.MinValue;
        Role = new Role();
        Services = new List<ApplicationService>();
        BoundServices = new List<Service>();
    }

    #endregion

    #region Props

    [MaxLength(10)] public string Passport { get; internal set; }
    public DateOnly Date { get; internal set; }
    public int RoleId { get; internal set; }
    public Role Role { get; internal set; }
    [MaxLength(64)]public string Password { get; internal set; }
    public decimal? Price { get; internal set; }

    public List<ApplicationService> Services { get; set; }
    public List<Service> BoundServices { get; set; }

    #endregion
}