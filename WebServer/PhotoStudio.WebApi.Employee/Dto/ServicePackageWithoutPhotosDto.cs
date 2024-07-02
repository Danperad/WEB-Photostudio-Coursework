namespace PhotoStudio.WebApi.Employee.Dto;

public class ServicePackageWithoutPhotosDto
{
    public int Id { get; set; }
    public string Title { get; internal set; }
    public string Description { get; internal set; }
    public decimal Cost { get; internal set; }
    public int Duration { get; set; }
}