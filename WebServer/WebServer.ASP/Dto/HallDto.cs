using System.Text.Json.Serialization;
using PhotostudioDB.Models;

namespace WebServer.ASP.Dto;

public class HallDto
{
    public HallDto(int id, string title, string description, int addressId, decimal pricePerHour,
        ICollection<string> photos)
    {
        Id = id;
        Title = title;
        Description = description;
        AddressId = addressId;
        PricePerHour = pricePerHour;
        Photos = photos;
    }

    [JsonPropertyName("id")] public int Id { get; internal set; }
    [JsonPropertyName("title")] public string Title { get; internal set; }
    [JsonPropertyName("description")] public string Description { get; internal set; }
    [JsonPropertyName("addressId")] public int AddressId { get; internal set; }
    [JsonPropertyName("cost")] public decimal PricePerHour { get; internal set; }
    [JsonPropertyName("photos")] public ICollection<string> Photos { get; internal set; }

    public static HallDto GetHallModel(Hall hall)
    {
        return new HallDto(hall.Id, hall.Title, hall.Description, hall.AddressId, hall.PricePerHour, hall.Photos);
    }
}