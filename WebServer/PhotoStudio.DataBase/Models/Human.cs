using System.ComponentModel.DataAnnotations;

namespace PhotoStudio.DataBase.Models;

public abstract class Human
{
    #region Props

    public int Id { get; internal set; }
    [MaxLength(50)] public string LastName { get; set; }
    [MaxLength(50)] public string FirstName { get; set; }
    [MaxLength(50)] public string? MiddleName { get; set; }
    [MaxLength(50)] public string EMail { get; set; }
    [MaxLength(16)] public string Phone { get; set; }

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