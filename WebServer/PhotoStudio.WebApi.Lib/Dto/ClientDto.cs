namespace PhotoStudio.WebApi.Lib.Dto;

public class ClientDto
{
    public ClientDto()
    {
        LastName = FirstName = "";
        EMail = Phone = "";
    }
    
    public int Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string EMail { get; set; }
    public string Phone { get; set; }
    public string? Avatar { get; set; }
}