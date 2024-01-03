using System.Text.Json.Serialization;

namespace WebServer.ASP.Dto;

public class NewOrderModel
{
    [JsonPropertyName("serviceModels")] public List<NewServiceModel> ServiceModels { get; set; } = new();
    [JsonPropertyName("servicePackage")] public NewServicePackageModel? ServicePackage { get; set; }
}