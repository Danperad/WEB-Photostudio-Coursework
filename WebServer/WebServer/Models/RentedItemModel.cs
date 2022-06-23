using System.Text.Json.Serialization;
using PhotostudioDB.Models;

namespace WebServer.Models;

public class RentedItemModel
{
    public RentedItemModel(int id, string title, decimal cost, uint number, byte type)
    {
        Id = id;
        Title = title;
        Cost = cost;
        Number = number;
        Type = type;
    }

    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("cost")] public decimal Cost { get; set; }
    [JsonPropertyName("number")] public uint Number { get; set; }
    [JsonPropertyName("typeItem")] public byte Type { get; set; }

    public static RentedItemModel GetModel(RentedItem item)
    {
        if (item.IsKids)
            return new RentedItemModel(item.Id, item.Title, item.Cost, item.Number, 3);
        return item.IsСlothes
            ? new RentedItemModel(item.Id, item.Title, item.Cost, item.Number, 2)
            : new RentedItemModel(item.Id, item.Title, item.Cost, item.Number, 1);
    }

    public static IEnumerable<RentedItemModel> GetModels(IEnumerable<RentedItem> items)
    {
        return items.Select(GetModel);
    }
}