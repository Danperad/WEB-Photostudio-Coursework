using System.ComponentModel.DataAnnotations;

namespace PhotoStudio.DataBase.Models;

public enum StatusType
{
    Order = 1,
    Service
}

public enum StatusValue
{
    Canceled = 1,
    NotAccepted = 2,
    InWork = 3,
    Done = 4,
    NotStarted = 5
}

public class Status
{
    internal Status()
    {
        Id = 0;
        Type = 0;
        Title = "";
    }

    public StatusValue Id { get; internal set; }
    public StatusType Type { get; internal set; }
    [MaxLength(50)] public string Title { get; internal set; }
    [MaxLength(300)] public string? Description { get; internal set; }
}