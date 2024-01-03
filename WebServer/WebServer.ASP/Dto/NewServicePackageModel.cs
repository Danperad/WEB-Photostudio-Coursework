using System.Text.Json.Serialization;

namespace WebServer.ASP.Dto;

public class NewServicePackageModel
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("startTime")] public long StartTime { get; set; }
}