using StreamingApp.BLL.Interfaces;

namespace StreamingApp.BLL.Requests;

[Serializable]
public class StartSharingRequest : RequestBase
{
    public int SenderId { get; set; }
    public int MeetingId { get; set; }
    public string IpAddress { get; set; } = null!;
    public int Port { get; set; }
}
