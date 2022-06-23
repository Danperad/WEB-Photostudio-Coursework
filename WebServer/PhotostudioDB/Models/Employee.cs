namespace PhotostudioDB.Models;

public class Employee : Human
{
    #region Ctors

    internal Employee()
    {
        Passport = "";
        Date = DateOnly.MinValue;
        Profile = new Profile();
        Role = new Role();
    }

    #endregion

    #region Props

    public string Passport { get; internal set; }
    public DateOnly Date { get; internal set; }
    public int RoleId { get; internal set; }
    public int ProfileId { get; internal set; }
    public Profile Profile { get; internal set; }
    public Role Role { get; internal set; }
    public decimal? Price { get; internal set; }

    #endregion

    #region Methods

    /*public static Employee? GetEmployeeById(int id)
    {
        return DbWorker.GetEmployeeById(id);
    }

    public static IEnumerable<Employee> GetEmployeeByRole(int role)
    {
        return DbWorker.GetEmployeesByRole(role);
    }*/

    #endregion
}