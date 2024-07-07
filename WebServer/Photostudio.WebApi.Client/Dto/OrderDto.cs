using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Client.Dto;

public class OrderDto
{
    public OrderDto()
    {
        Services = new List<OrderServiceDto>();
    }
    public int Number { get; set; }
    public DateTime DateTime { get; set; }
    public IReadOnlyList<OrderServiceDto> Services { get; set; }
    public StatusDto? Status { get; set; }
    public decimal? TotalPrice { get; set; }
    public ServicePackageDto? ServicePackage { get; set; }
}