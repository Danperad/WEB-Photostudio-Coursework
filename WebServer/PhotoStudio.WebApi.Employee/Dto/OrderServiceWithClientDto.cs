using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Employee.Dto;

public class OrderServiceWithClientDto
{
    public int? Id { get; set; }
    public int OrderStatus { get; set; }
    public ClientDto Client { get; set; } = null!;
    public SimpleServiceDto Service { get; set; } = null!;
    public RentedItemDto? Item { get; set; }
    public EmployeeDto Employee { get; set; } = null!;
    public int? Count { get; set; }
    public HallDto? Hall { get; set; }
    public DateTime? StartDateTime { get; set; }
    public int? Duration { get; set; }
    public bool? IsFullTime { get; set; }
    public StatusDto? Status { get; set; }
    public decimal Cost { get; set; }
}