namespace StreamingApp.BLL.Requests;

[Serializable]
public class LoginRequest : RequestBase
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}
