using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Requests;

[Serializable]
public class RegisterRequest : RequestBase
{
    public User User { get; set; } = null!;
}
