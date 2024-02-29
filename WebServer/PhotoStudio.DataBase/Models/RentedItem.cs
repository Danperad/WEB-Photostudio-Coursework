namespace PhotoStudio.DataBase.Models;

public class RentedItem : IServiced
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
        Services = new List<ApplicationService>();
    }

    #region Props

    public int Id { get; internal set; }
    public string Title { get; internal set; }
    public string Description { get; internal set; }
    public decimal Cost { get; internal set; }
    public uint Number { get; internal set; }
    public bool IsСlothes { get; internal set; }
    public bool IsKids { get; internal set; }

    public ICollection<ApplicationService> Services { get; set; }
    #endregion

}