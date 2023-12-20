namespace StreamingApp.BLL.Responses;

public class ErrorResponse : ResponseBase
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; } = null!;
}
