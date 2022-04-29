using PhotostudioDB.Exceptions;

namespace PhotostudioDB.Models;

public class Hall
{
    #region Props

    public int Id { get; internal set; }
    public string Title { get; internal set; }
    public string Description { get; internal set; }
    public int AddressId { get; internal set; }
    public Address Address { get; internal set; }

    #endregion

    #region Ctors

    internal Hall()
    {
        Id = 0;
        Title = "";
        Description = "";
        Address = new Address();
    }

    #endregion
}