namespace PhotoStudio.WebApi.Lib.Dto;

public class SimpleServiceDto
{
    public int Id { get; set; }
    public string Title { get; internal set; }
    public string Description { get; internal set; }
    public decimal Cost { get; internal set; }
    public int Type { get; internal set; }
}