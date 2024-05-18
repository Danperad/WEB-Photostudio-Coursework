namespace PhotoStudio.WebApi.Employee.Dto;

public enum ItemType
{
    Simple = 1,
    Cloth,
    KidsCloth
}

public class RentedItemDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ItemType Type { get; set; }
    public decimal CostPerHour { get; set; }
}