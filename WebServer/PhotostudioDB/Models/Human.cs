namespace PhotostudioDB.Models;

public abstract class Human
{
    #region Metods

    protected bool Check()
    {
        return (Phone.Length >= 6) & (FirstName.Length > 1) && LastName.Length > 1 &&
               (EMail is null || EMail.Length > 5) && (MiddleName is null || MiddleName.Length > 5);
    }

    #endregion

    #region Props

    public int Id { get; internal set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string EMail { get; set; }
    public string Phone { get; set; }

    public string FullName => MiddleName is null ? $"{LastName} {FirstName[..1]}." 
        : $"{LastName} {FirstName[..1]}. {MiddleName[..1]}.";

    #endregion

    #region Ctors

    protected Human()
    {
        Id = 0;
        LastName = "";
        FirstName = "";
        Phone = "";
        EMail = "";
    }

    protected Human(string lastName, string firstName, string phone)
    {
        LastName = lastName;
        FirstName = firstName;
        Phone = phone;
        EMail = "";
    }

    #endregion
}