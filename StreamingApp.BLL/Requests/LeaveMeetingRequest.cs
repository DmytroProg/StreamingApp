using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Requests;

[Serializable]
public class LeaveMeetingRequest : RequestBase
{
    public User User { get; set; } = null!;
    public int MeetingId { get; set; }
}
