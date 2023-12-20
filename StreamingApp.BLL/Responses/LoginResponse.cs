using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Responses;

[Serializable]
public class LoginResponse : ResponseBase
{
    public User User { get; set; } = null!;
}
