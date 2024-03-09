using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Requests;

[Serializable]
public class CreateMeeting : RequestBase
{
    public Meeting Meeting { get; set; } = null!;
}
