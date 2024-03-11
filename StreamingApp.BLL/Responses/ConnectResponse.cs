using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Responses;

[Serializable]
public class ConnectResponse : ResponseBase
{
    public User ConnectedUser { get; set; } = null!;
    public Meeting Meeting { get; set; } = null!;
}
