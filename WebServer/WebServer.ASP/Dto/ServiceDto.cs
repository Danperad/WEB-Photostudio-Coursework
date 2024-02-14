using System.Text.Json.Serialization;
using PhotostudioDB.Models;

namespace WebServer.ASP.Dto;

public class ServiceDto
{
    private ServiceDto(int id, string title, string description, decimal cost, List<string> photos, int type)
    {
        Id = id;
        Title = title;
        Description = description;
        Cost = cost;
        Photos = photos;
        Type = type;
    }

    public int Id { get; set; }
    public string Title { get; internal set; }
    public string Description { get; internal set; }
    public decimal Cost { get; internal set; }
    public List<string> Photos { get; internal set; }
    public int Type { get; internal set; }

    public static ServiceDto GetServiceModel(Service service)
    {
        return new ServiceDto(service.Id, service.Title, service.Description, service.Cost, service.Photos,
            (int) service.Type);
    }
}