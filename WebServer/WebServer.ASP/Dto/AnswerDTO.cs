namespace WebServer.ASP.Dto;

public record AnswerDto(bool Status, object? Answer, int? Error);