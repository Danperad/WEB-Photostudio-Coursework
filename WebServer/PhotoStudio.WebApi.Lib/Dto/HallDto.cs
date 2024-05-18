namespace PhotoStudio.WebApi.Lib.Dto;

public class HallDto
{
    public HallDto(int id, string title, string description, decimal pricePerHour,
        ICollection<string> photos)
    {
        Id = id;
        Title = title;
        Description = description;
        PricePerHour = pricePerHour;
        Photos = photos;
    }

    public int Id { get; internal set; }
    public string Title { get; internal set; }
    public string Description { get; internal set; }
    public decimal PricePerHour { get; internal set; }
    public ICollection<string> Photos { get; internal set; }
}