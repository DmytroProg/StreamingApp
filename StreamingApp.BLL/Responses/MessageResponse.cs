using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Responses;

[Serializable]
public class MessageResponse : ResponseBase
{
    public MessageBase Message { get; set; } = null!;
}
