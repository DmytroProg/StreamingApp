namespace StreamingApp.BLL.Requests;

[Serializable]
public class LogoutRequest : RequestBase
{
    public int UserId { get; set; }
}
