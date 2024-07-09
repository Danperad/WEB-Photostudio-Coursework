namespace PhotoStudio.WebApi.Lib.Dto;

public class AnswerDto(bool status, object? answer, int? error)
{
    public bool Status { get; set; } = status;
    public object? Answer { get; set; } = answer;
    public int? Error { get; set; } = error;
}