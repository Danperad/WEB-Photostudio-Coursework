using System.ComponentModel.DataAnnotations;

namespace PhotoStudio.DataBase.Models;

public class RefreshToken
{
    internal RefreshToken()
    {
        Token = "";
        Client = new Client();
    }

    public RefreshToken(string token, Client client, int duration)
    {
        Token = token;
        Client = client;
        SignDate = DateTime.Now;
        EndDate = SignDate.AddDays(duration);
    }

    [Key] public string Token { get; internal set; }

    public Client Client { get; internal set; }
    public int ClientId { get;internal set; }
    public DateTime SignDate { get; internal set; }
    public DateTime EndDate { get; internal set; }
}