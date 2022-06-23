using System.ComponentModel.DataAnnotations;

namespace PhotostudioDB.Models;

public class RefreshToken
{
    internal RefreshToken()
    {
        Token = "";
        Profile = new Profile();
    }

    public RefreshToken(string token, Profile profile)
    {
        Token = token;
        Profile = profile;
        SignDate = DateTime.Now;
    }

    [Key] public string Token { get; internal set; }

    public Profile Profile { get; internal set; }
    public int ProfileId { get;internal set; }
    public DateTime SignDate { get; internal set; }

    // public static bool AddToken(string token, Profile profile)
    // {
    //     var jwtToken = new RefreshToken(token, profile);
    //     return DbWorker.AddToken(jwtToken);
    // }
    //
    // public static Profile? ContainsToken(string token)
    // {
    //     DbWorker.RemoveTokens();
    //     return DbWorker.GetToken(token);
    // }
}