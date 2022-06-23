using System.Text.Json.Serialization;

namespace WebServer.Models;

public class AuthModel
{
    public AuthModel()
    {
        Login = Password = "";
    }

    [JsonPropertyName("login")] public string Login { get; set; }
    [JsonPropertyName("password")] public string Password { get; set; }
}