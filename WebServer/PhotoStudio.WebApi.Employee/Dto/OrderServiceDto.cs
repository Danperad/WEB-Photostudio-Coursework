using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Employee.Dto;

public class OrderServiceDto
{
    public int? Id { get; set; }
    public SimpleServiceDto Service { get; set; }
    public EmployeeDto? Employee { get; set; }
    public RentedItemDto? Item { get; set; }
    public int? Count { get; set; }
    public HallDto? Hall { get; set; }
    public DateTime? StartDateTime { get; set; }
    public int? Duration { get; set; }
    public bool? IsFullTime { get; set; }
    public StatusDto? Status { get; set; }
    public decimal Cost { get; set; }
}