using System.ComponentModel.DataAnnotations;

namespace PhotostudioDB.Models;

public class AuthToken
{
    [Key]
    public string Token { get; set; }
    public DateTime TokenTime { get; set; }

    internal AuthToken()
    {
        Token = "";
        TokenTime = DateTime.MinValue;
    }

    private AuthToken(string token)
    {
        Token = token;
        TokenTime = DateTime.Now;
    }

    public static bool AddToken(string token)
    {
        var jwtToken = new AuthToken(token);
        return DbWorker.AddToken(jwtToken);
    }

    public static bool RemoveLastTokens(TimeSpan timeSpan)
    {
        return DbWorker.RemoveToken(timeSpan);
    }

    public static bool ContainsToken(string token)
    {
        return DbWorker.GetToken(token);
    }
}