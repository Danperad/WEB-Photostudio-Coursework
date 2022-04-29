namespace PhotostudioDB.Models;

public class Address
{
    #region Props

    public int Id { get; internal set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string? Building { get; set; }
    public string? Entrance { get; set; }
    public string? ApartmentNumber { get; set; }

    #endregion

    #region Ctors
    
    internal Address()
    {
        Id = 0;
        Street = "";
        HouseNumber = "";
    }
    
    public Address(string street, string houseNumber)
    {
        Street = street;
        HouseNumber = houseNumber;
    }

    #endregion

    #region Methods
    
    public bool AddAddress()
    {
        return DbWorker.AddAddress(this);
    }
    
    public static Address? GetAddressById(int id)
    {
        return DbWorker.GetAddressById(id);
    }

    #endregion
}