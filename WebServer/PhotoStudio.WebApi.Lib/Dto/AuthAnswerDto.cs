namespace PhotoStudio.WebApi.Lib.Dto;

public record AuthAnswerDto(string AccessToken, string RefreshToken, ClientDto User);