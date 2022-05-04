using System.ComponentModel.DataAnnotations;

namespace PhotostudioDB.Models;

public class RefreshToken
{
    [Key]
    public string Token { get; set; }
    public Profile Profile { get; set; }

    internal RefreshToken()
    {
        Token = "";
        Profile = new Profile();
    }

    private RefreshToken(string token, Profile profile)
    {
        Token = token;
        Profile = profile;
    }

    public static bool AddToken(string token, Profile profile)
    {
        var jwtToken = new RefreshToken(token, profile);
        return DbWorker.AddToken(jwtToken);
    }

    public static Profile? ContainsToken(string token)
    {
        return DbWorker.GetToken(token);
    }
}