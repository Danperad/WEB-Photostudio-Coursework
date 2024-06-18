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
    private ServicePackageDto(int id, string title, string description, decimal cost, List<string> photos, int duration)
    {
        Id = id;
        Title = title;
        Description = description;
        Cost = cost;
        Photos = photos;
        Duration = duration;
    }
    
    public int Id { get; set; }
    public string Title { get; internal set; }
    public string Description { get; internal set; }
    public decimal Cost { get; internal set; }
    public List<string> Photos { get; internal set; }
    public int Duration { get; set; }
}