namespace PhotostudioDB.Models;

public class Role
{
    internal Role()
    {
        Id = 0;
        Title = "";
        Description = "";
    }

    public int Id { get; internal set; }
    public string Title { get; internal set; }
    public string Description { get; internal set; }
}