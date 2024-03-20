using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Responses;

[Serializable]
public class MessageResponse : ResponseBase
{
    public Meeting Meeting { get; set; } = null!;
}
