using System.Text.Json.Serialization;
using PhotostudioDB.Models;

namespace WebServer.Models;

public class ClientModel
{
    public ClientModel()
    {
        Id = 0;
        LastName = FirstName = EMail = Phone = Login = "";
        MiddleName = Avatar = "";
    }

    private ClientModel(int id, string lastName, string firstName, string? middleName, string eMail, string phone,
        string login, string? avatar)
    {
        Id = id;
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        EMail = eMail;
        Phone = phone;
        Login = login;
        Avatar = avatar;
    }

    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("lastName")] public string LastName { get; set; }
    [JsonPropertyName("firstName")] public string FirstName { get; set; }
    [JsonPropertyName("middleName")] public string? MiddleName { get; set; }
    [JsonPropertyName("email")] public string EMail { get; set; }
    [JsonPropertyName("phone")] public string Phone { get; set; }
    [JsonPropertyName("login")] public string Login { get; set; }
    [JsonPropertyName("avatar")] public string? Avatar { get; set; }

    public static ClientModel GetClientModel(Client client)
    {
        return new ClientModel(client.Id, client.LastName, client.FirstName, client.MiddleName, client.EMail!,
            client.Phone, client.Profile!.Login, client.Avatar);
    }
}