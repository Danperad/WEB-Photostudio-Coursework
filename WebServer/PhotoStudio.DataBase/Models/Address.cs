using System.ComponentModel.DataAnnotations;

namespace PhotoStudio.DataBase.Models;

public class Address(string city, string street, string houseNumber)
{
    #region Props

    public int Id { get; internal set; }
    [MaxLength(50)] public string City { get; set; } = city;
    [MaxLength(50)] public string Street { get; set; } = street;
    [MaxLength(6)] public string HouseNumber { get; set; } = houseNumber;
    [MaxLength(6)] public string? ApartmentNumber { get; set; }

    #endregion

    #region Ctors

    internal Address() : this("", "", "")
    {
        Id = 0;
    }

    #endregion
}