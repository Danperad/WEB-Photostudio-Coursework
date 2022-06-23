namespace PhotostudioDB.Models;

public class RentedItem
{
    internal RentedItem()
    {
        Id = 0;
        Title = "";
        Description = "";
        Cost = 0;
        Number = 0;
        IsСlothes = false;
        IsKids = false;
    }

    #region Props

    public int Id { get; internal set; }
    public string Title { get; internal set; }
    public string Description { get; internal set; }
    public decimal Cost { get; internal set; }
    public uint Number { get; internal set; }
    public bool IsСlothes { get; internal set; }
    public bool IsKids { get; internal set; }

    #endregion
}