using System.Text.Json.Serialization;

namespace WebServer.Models;

public class NewServicePackageModel
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("addressId")] public int? AddressId { get; set; }
    [JsonPropertyName("hallId")] public int? HallId { get; internal set; }
    [JsonPropertyName("employeeId")] public int? EmployeeId { get; set; }
}