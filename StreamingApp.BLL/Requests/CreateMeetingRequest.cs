using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Requests;

[Serializable]
public class CreateMeetingRequest : RequestBase
{
    public Meeting Meeting { get; set; } = null!;
    public int SharingPort { get; set; }
}
