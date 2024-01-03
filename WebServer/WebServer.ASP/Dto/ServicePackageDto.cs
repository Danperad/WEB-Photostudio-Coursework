using System.Text.Json.Serialization;
using PhotostudioDB.Models;

namespace WebServer.ASP.Dto;

public class ServicePackageDto
{
    private ServicePackageDto(int id, string title, string description, decimal cost, List<string> photos, double rating, int duration)
    {
        Id = id;
        Title = title;
        Description = description;
        Cost = cost;
        Photos = photos;
        Rating = rating;
        Duration = duration;
    }
    
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("title")] public string Title { get; internal set; }
    [JsonPropertyName("description")] public string Description { get; internal set; }
    [JsonPropertyName("cost")] public decimal Cost { get; internal set; }
    [JsonPropertyName("photos")] public List<string> Photos { get; internal set; }
    [JsonPropertyName("rating")] public double Rating { get; internal set; }
    [JsonPropertyName("duration")] public int Duration { get; set; }

    
    public static ServicePackageDto GetServiceModel(ServicePackage service)
    {
        return new ServicePackageDto(service.Id, service.Title, service.Description, service.Price, service.Photos, service.Rating, service.Duration);
    }
}