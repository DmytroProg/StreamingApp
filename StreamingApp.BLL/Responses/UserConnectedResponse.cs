using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Responses;

[Serializable]
public class UserConnectedResponse : ResponseBase
{
    public User ConnectedUser { get; set; } = null!;
}
