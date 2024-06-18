using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Client.Dto;

public class OrderDto
{
    public int Number { get; set; }
    public DateTime DateTime { get; set; }
    public List<OrderServiceDto> Services { get; set; }
    public StatusDto? Status { get; set; }
    public decimal? TotalPrice { get; set; }
    public ServicePackageDto? ServicePackage { get; set; }
}