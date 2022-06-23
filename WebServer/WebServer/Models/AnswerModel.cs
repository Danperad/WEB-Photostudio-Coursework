using System.Text.Json.Serialization;

namespace WebServer.Models;

public class AnswerModel
{
    public AnswerModel()
    {
        Status = false;
        Answer = null;
        Error = 404;
    }

    public AnswerModel(bool status, object? answer, int? error)
    {
        Status = status;
        Answer = answer;
        Error = error;
    }

    [JsonPropertyName("status")] public bool Status { get; set; }
    [JsonPropertyName("answer")] public object? Answer { get; set; }
    [JsonPropertyName("error")] public int? Error { get; set; }
}