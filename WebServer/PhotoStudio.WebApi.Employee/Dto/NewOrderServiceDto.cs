namespace PhotoStudio.WebApi.Employee.Dto;

public class NewOrderServiceDto
{
    public int? Id { get; set; }
    public int Service { get; set; }
    public int? Employee { get; set; }
    public int? Item { get; set; }
    public int? Count { get; set; }
    public int? Hall { get; set; }
    public DateTime? StartDateTime { get; set; }
    public int? Duration { get; set; }
    public bool? IsFullTime { get; set; }
}