using System.Text.Json.Serialization;

namespace WebServer.Models;

public class RegModel
{
    public RegModel()
    {
        Login = Password = LastName = FirstName = MiddleName = Phone = EMail = "";
    }

    [JsonPropertyName("login")] public string Login { get; set; }
    [JsonPropertyName("password")] public string Password { get; set; }
    [JsonPropertyName("lastName")] public string LastName { get; set; }
    [JsonPropertyName("firstName")] public string FirstName { get; set; }
    [JsonPropertyName("middleName")] public string MiddleName { get; set; }
    [JsonPropertyName("phone")] public string Phone { get; set; }
    [JsonPropertyName("eMail")] public string EMail { get; set; }

    public static bool Check(RegModel? model)
    {
        return model is null || string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password) ||
               string.IsNullOrEmpty(model.LastName) || string.IsNullOrEmpty(model.FirstName) ||
               string.IsNullOrEmpty(model.Phone) || string.IsNullOrEmpty(model.EMail);
    }
}