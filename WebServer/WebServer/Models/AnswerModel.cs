namespace WebServer.Models;

public class AnswerModel
{
    public bool status { get; set; }
    public object? answer { get; set; }
    public int? error { get; set; }
    public string? errorText { get; set; }

    public AnswerModel()
    {
        status = false;
        answer = null;
        error = 404;
    }

    public AnswerModel(bool status, object? answer, int? error, string? errorText)
    {
        this.status = status;
        this.answer = answer;
        this.error = error;
        this.errorText = errorText;
    }
}