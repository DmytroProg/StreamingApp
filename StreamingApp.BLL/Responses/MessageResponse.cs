using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Responses;

[Serializable]
public class MessageResponse : ResponseBase
{
    public User Sender { get; set; } = null!;
    public MessageBase Message { get; set; } = null!;
}
