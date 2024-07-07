using System.Text.Json.Serialization;

namespace PhotoStudio.WebApi.Lib.Dto;

public class NewOrderModel
{
    [JsonPropertyName("serviceModels")]
    public IReadOnlyList<NewServiceModel> ServiceModels { get; set; } = new List<NewServiceModel>();
    [JsonPropertyName("servicePackage")] public NewServicePackageModel? ServicePackage { get; set; }
}