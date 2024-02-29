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
    }

    #endregion

    #region Props

    public string Passport { get; internal set; }
    public DateOnly Date { get; internal set; }
    public int RoleId { get; internal set; }
    public Role Role { get; internal set; }
    public string Password { get; internal set; }
    public decimal? Price { get; internal set; }
    
    public ICollection<ApplicationService> Services { get; set; }

    #endregion

}