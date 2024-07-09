namespace PhotoStudio.WebApi.Lib.Dto;

public class AuthAnswerDto(string accessToken, string refreshToken, ClientDto user)
{
    public string AccessToken { get; set; } = accessToken;
    public string RefreshToken { get; set; } = refreshToken;
    public ClientDto User { get; set; } = user;
};