namespace PhotoStudio.WebApi.Lib.Dto;

public class ServicePackageDto
{
    public ServicePackageDto()
    {
        Id = 0;
        Title = "";
        Description = "";
        Cost = 0;
        Photos = new List<string>();
        Duration = 0;
    }
    
    public int Id { get; set; }
    public string Title { get; internal set; }
    public string Description { get; internal set; }
    public decimal Cost { get; internal set; }
    public IReadOnlyList<string> Photos { get; internal set; }
    public int Duration { get; set; }
}