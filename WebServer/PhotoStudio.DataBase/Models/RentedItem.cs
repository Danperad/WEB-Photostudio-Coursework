using System.ComponentModel.DataAnnotations;

namespace PhotoStudio.DataBase.Models;

public enum ItemType
{
    Simple = 1,
    Cloth,
    KidsCloth
}

public class RentedItem : IServiced
{
    internal RentedItem()
    {
        Id = 0;
        Title = "";
        Description = "";
        Cost = 0;
        Number = 0;
        Type = ItemType.Simple;
        Services = new List<ApplicationService>();
    }

    #region Props

    public int Id { get; internal set; }
    [MaxLength(50)]public string Title { get; internal set; }
    [MaxLength(300)]public string Description { get; internal set; }
    public decimal Cost { get; internal set; }
    public uint Number { get; internal set; }
    public ItemType Type { get; internal set; }

    public List<ApplicationService> Services { get; set; }

    #endregion
}