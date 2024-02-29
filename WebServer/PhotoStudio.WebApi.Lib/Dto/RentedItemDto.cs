using PhotoStudio.DataBase.Models;

namespace PhotoStudio.WebApi.Lib.Dto;

public class RentedItemDto(int id, string title, decimal cost, uint number, byte type)
{
    public int Id { get; set; } = id;
    public string Title { get; set; } = title;
    public decimal Cost { get; set; } = cost;
    public uint Number { get; set; } = number;
    public byte Type { get; set; } = type;

    public static RentedItemDto GetModel(RentedItem item)
    {
        if (item.IsKids)
            return new RentedItemDto(item.Id, item.Title, item.Cost, item.Number, 3);
        return item.IsСlothes
            ? new RentedItemDto(item.Id, item.Title, item.Cost, item.Number, 2)
            : new RentedItemDto(item.Id, item.Title, item.Cost, item.Number, 1);
    }
}