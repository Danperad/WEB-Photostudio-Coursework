namespace PhotoStudio.WebApi.Employee.Dto;

public class OrderReportDto
{
    public OrderReportDto()
    {
        Client = new ClientReportDto();
        Status = "";
        Services = new List<ApplicationServiceReportDto>();
    }
    public int Number { get; set; }
    public ClientReportDto Client { get; set; }
    public DateTime DateTime { get; set; }
    public string Status { get; set; }
    public decimal TotalPrice { get; set; }
    public ServicePackageReportDto? ServicePackage { get; set; }
    public IReadOnlyList<ApplicationServiceReportDto> Services { get; set; }
}

public class ClientReportDto
{
    public ClientReportDto()
    {
        LastName = FirstName = "";
    }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
}

public class ServicePackageReportDto
{
    public ServicePackageReportDto()
    {
        Title = "";
    }
    public string Title { get; set; }
    public decimal Price { get; set; }
}

public class ApplicationServiceReportDto
{
    public ApplicationServiceReportDto()
    {
        Service = "";
        Employee = new EmployeeReportDto();
        Status = "";
    }
    public int Id { get; set; }
    public string Service { get; set; }
    public EmployeeReportDto Employee { get; set; }
    public string Status { get; set; }
    public DateTime? StartDateTime { get; set; }
    public TimeSpan? Duration { get; set; }
    public decimal Cost { get; set; }
    public string? Hall { get; set; }
    public string? Item { get; set; }
    public int? Number { get; internal set; }
    public bool? IsFullTime { get; set; }
    public ServicePackageReportDto? BingingPackage { get; set; }
}

public class EmployeeReportDto
{
    public EmployeeReportDto()
    {
        LastName = "";
        FirstName = "";
    }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
}