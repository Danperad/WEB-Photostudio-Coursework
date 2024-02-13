using System.Text.Json.Serialization;
using PhotostudioDB.Models;

namespace WebServer.ASP.Dto;

public class ServicePackageDto
{
    private ServicePackageDto(int id, string title, string description, decimal cost, List<string> photos, int duration)
    {
        Id = id;
        Title = title;
        Description = description;
        Cost = cost;
        Photos = photos;
        Duration = duration;
    }
    
    public int Id { get; set; }
    public string Title { get; internal set; }
    public string Description { get; internal set; }
    public decimal Cost { get; internal set; }
    public List<string> Photos { get; internal set; }
    public int Duration { get; set; }

    
    public static ServicePackageDto GetServiceModel(ServicePackage service)
    {
        return new ServicePackageDto(service.Id, service.Title, service.Description, service.Price, service.Photos, service.Duration);
    }
}