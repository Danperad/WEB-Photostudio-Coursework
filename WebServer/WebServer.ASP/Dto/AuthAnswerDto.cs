namespace WebServer.ASP.Dto;

public record AuthAnswerDto(string AccessToken, string RefreshToken, ClientDto User);