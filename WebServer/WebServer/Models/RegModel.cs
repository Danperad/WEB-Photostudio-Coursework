namespace WebServer.Models;

public class RegModel
{
    public string login { get; set; }
    public string password { get; set; }
    public string lastname { get; set; }
    public string firstname { get; set; }
    public string middlename { get; set; }
    public string phone { get; set; }
    public string email { get; set; }

    public static bool Check(RegModel? model)
    {
        return model is not null && string.IsNullOrEmpty(model.login) && string.IsNullOrEmpty(model.password) &&
               string.IsNullOrEmpty(model.lastname) && string.IsNullOrEmpty(model.firstname) &&
               string.IsNullOrEmpty(model.phone);
    }
}