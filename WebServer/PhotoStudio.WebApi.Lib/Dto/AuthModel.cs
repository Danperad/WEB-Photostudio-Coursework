namespace PhotoStudio.WebApi.Lib.Dto;

public class AuthModel
{
    public AuthModel()
    {
        Login = Password = "";
    }

    public string Login { get; set; }
    public string Password { get; set; }
}