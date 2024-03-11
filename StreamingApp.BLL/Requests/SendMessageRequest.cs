using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Requests;

[Serializable]
public class SendMessageRequest : RequestBase
{
    public User Sender { get; set; } = null!;
    public int MeetingId { get; set; }
    public MessageBase Message { get; set; } = null!;
}
