using PhotoStudio.WebApi.Lib.Dto;

namespace PhotoStudio.WebApi.Employee.Dto;

public class OrderDto
{
    public int Number { get; set; }
    public ClientDto Client { get; set; }
    public DateTime DateTime { get; set; }
    public StatusDto? Status { get; set; }
    public decimal? TotalPrice { get; set; }
}