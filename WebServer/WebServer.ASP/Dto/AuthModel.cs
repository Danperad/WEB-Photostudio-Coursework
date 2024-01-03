using System.Text.Json.Serialization;

namespace WebServer.ASP.Dto;

public class AuthModel
{
    public AuthModel()
    {
        Login = Password = "";
    }

    public string Login { get; set; }
    public string Password { get; set; }
}