namespace PhotostudioDB.Exceptions;

public class NotInDbException : Exception
{
    public NotInDbException(string prop, string message = "Object is not in the database") : base(message)
    {
        Prop = prop;
    }

    public string Prop { get; }
}