using System.ComponentModel.DataAnnotations;

namespace PhotoStudio.DataBase.Models;

public class Role
{
    internal Role()
    {
        Id = 0;
        Title = "";
        Description = "";
    }

    public int Id { get; internal set; }
    [MaxLength(50)] public string Title { get; internal set; }
    [MaxLength(300)] public string Description { get; internal set; }
}