namespace PhotostudioDB.Exceptions;

public class ValueNotFoundException : Exception
{
    public ValueNotFoundException(string prop, string msg = "Current value not found") : base(msg)
    {
        Prop = prop;
    }

    public string Prop { get; }
}