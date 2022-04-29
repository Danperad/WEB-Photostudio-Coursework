namespace PhotostudioDB.Models;

public class Status
{
    internal Status()
    {
        Id = 0;
        Type = 0;
        Title = "";
    }

    public int Id { get; internal set; }
    public int Type { get; internal set; }
    public string Title { get; internal set; }
    public string? Description { get; internal set; }
}