using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Responses;

[Serializable]
public class ConnectResponse : ResponseBase
{
    public Meeting Meeting { get; set; } = null!;
}
