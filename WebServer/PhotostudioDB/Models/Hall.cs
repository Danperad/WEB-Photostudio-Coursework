namespace PhotostudioDB.Models;

public class Hall
{
    #region Ctors

    internal Hall()
    {
        Id = 0;
        Title = "";
        Description = "";
        Address = new Address();
        Photos = new List<string>();
    }

    #endregion

    #region Props

    public int Id { get; internal set; }
    public string Title { get; internal set; }
    public string Description { get; internal set; }
    public int AddressId { get; internal set; }
    public Address Address { get; internal set; }
    public decimal PricePerHour { get; internal set; }
    public List<string> Photos { get; internal set; }

    #endregion
}