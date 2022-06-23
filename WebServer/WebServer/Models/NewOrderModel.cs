using System.Text.Json.Serialization;

namespace WebServer.Models;

public class NewOrderModel
{
    [JsonPropertyName("serviceModels")] public List<NewServiceModel> ServiceModels { get; set; }
    [JsonPropertyName("servicePackage")] public NewServicePackageModel? ServicePackage { get; set; }

    public NewOrderModel()
    {
        ServiceModels = new List<NewServiceModel>();
    }
}