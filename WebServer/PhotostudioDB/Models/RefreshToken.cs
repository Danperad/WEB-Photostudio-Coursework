using System.ComponentModel.DataAnnotations;

namespace PhotostudioDB.Models;

public class RefreshToken
{
    [Key]
    internal string Token { get; set; }
    internal Profile Profile { get; set; }
    internal int ProfileId { get; set; }

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