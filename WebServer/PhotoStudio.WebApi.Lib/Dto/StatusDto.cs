namespace PhotoStudio.WebApi.Lib.Dto;

public class StatusDto
{
    public StatusDto()
    {
        Title = "";
    }
    public int Id { get; internal set; }
    public int Type { get; internal set; }
    public string Title { get; internal set; }
    public string? Description { get; internal set; }
}