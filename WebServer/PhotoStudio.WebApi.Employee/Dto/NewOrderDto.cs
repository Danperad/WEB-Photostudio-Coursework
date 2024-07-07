namespace PhotoStudio.WebApi.Employee.Dto;

public class NewOrderDto
{
    public NewOrderDto()
    {
        Services = new List<NewOrderServiceDto>();
    }
    public int Client { get; set; }
    public NewOrderServicePackageDto? ServicePackage { get; set; }
    public IReadOnlyList<NewOrderServiceDto> Services { get; set; }
}