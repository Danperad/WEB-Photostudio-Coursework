using System.Text.Json.Serialization;
using PhotostudioDB.Models;

namespace WebServer.Models;

public class EmployeeModel
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("cost")] public decimal Cost { get; set; }

    public EmployeeModel()
    {
        Id = 0;
        Title = "";
        Cost = 0;
    }
    public static EmployeeModel GetModel(Employee item)
    {
        return new EmployeeModel {Id = item.Id, Cost = item.Price!.Value, Title = item.FullName};
    }

    public static IEnumerable<EmployeeModel> GetModels(IEnumerable<Employee> items)
    {
        return items.Select(GetModel);
    }
}