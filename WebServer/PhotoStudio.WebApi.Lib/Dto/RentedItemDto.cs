namespace PhotoStudio.WebApi.Lib.Dto;

public class RentedItemDto(int id, string title, decimal cost, uint number, int type)
{
    public int Id { get; set; } = id;
    public string Title { get; set; } = title;
    public decimal Cost { get; set; } = cost;
    public uint Number { get; set; } = number;
    public int Type { get; set; } = type;
}