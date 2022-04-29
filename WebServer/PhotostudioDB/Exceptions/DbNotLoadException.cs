namespace PhotostudioDB.Exceptions;

public class DbNotLoadException : Exception
{
    public DbNotLoadException(string msg = "DataBase not load, please check ConnectionString") : base(msg)
    {
    }
}