using System.Text.Json.Serialization;
using PhotostudioDB.Models;

namespace WebServer.ASP.Dto;

public class ClientDto
{
    private ClientDto(int id, string lastName, string firstName, string? middleName, string eMail, string phone,
        string? avatar)
    {
        Id = id;
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        EMail = eMail;
        Phone = phone;
        Avatar = avatar;
    }

    public int Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string EMail { get; set; }
    public string Phone { get; set; }
    public string? Avatar { get; set; }
}