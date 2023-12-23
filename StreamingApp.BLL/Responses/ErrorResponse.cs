namespace StreamingApp.BLL.Responses;

[Serializable]
public class ErrorResponse : ResponseBase
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; } = null!;
}
