using StreamingApp.BLL.Interfaces;

namespace StreamingApp.BLL.Requests;

[Serializable]
public class StartSharingRequest : RequestBase
{
    public int SenderId { get; set; }
    public int MeetingId { get; set; }
    public int SegmentsCount { get; set; }
}
