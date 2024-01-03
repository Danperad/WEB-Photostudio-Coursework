using System.Text.Json.Serialization;
using PhotostudioDB.Models;

namespace WebServer.ASP.Dto;

public class ServiceDto
{
    private ServiceDto(int id, string title, string description, decimal cost, List<string> photos, int type, double rating)
    {
        Id = id;
        Title = title;
        Description = description;
        Cost = cost;
        Photos = photos;
        Type = type;
        Rating = rating;
    }

    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("title")] public string Title { get; internal set; }
    [JsonPropertyName("description")] public string Description { get; internal set; }
    [JsonPropertyName("cost")] public decimal Cost { get; internal set; }
    [JsonPropertyName("photos")] public List<string> Photos { get; internal set; }
    [JsonPropertyName("serviceType")] public int Type { get; internal set; }
    [JsonPropertyName("rating")] public double Rating { get; internal set; }

    public static ServiceDto GetServiceModel(Service service)
    {
        return new ServiceDto(service.Id, service.Title, service.Description, service.Cost, service.Photos,
            (int) service.Type, service.Rating);
    }
}