using System.Text.Json.Serialization;
using PhotostudioDB.Models;

namespace WebServer.Models;

public class ServiceModel
{
    public ServiceModel()
    {
        Title = "";
        Description = "";
        Photos = new List<string>();
    }
    private ServiceModel(int id, string title, string description, decimal cost, List<string> photos, int type)
    {
        Id = id;
        Title = title;
        Description = description;
        Cost = cost;
        Photos = photos;
        Type = type;
    }

    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("title")] public string Title { get; internal set; }
    [JsonPropertyName("description")] public string Description { get; internal set; }
    [JsonPropertyName("cost")] public decimal Cost { get; internal set; }
    [JsonPropertyName("photos")] public List<string> Photos { get; internal set; }
    [JsonPropertyName("serviceType")] public int Type { get; internal set; }

    public static ServiceModel GetServiceModel(Service service)
    {
        return new ServiceModel(service.Id, service.Title, service.Description, service.Cost, service.Photos,
            service.Type);
    }

    public static IEnumerable<ServiceModel> ConvertList(IEnumerable<Service> services)
    {
        return services.Select(GetServiceModel);
    }
}