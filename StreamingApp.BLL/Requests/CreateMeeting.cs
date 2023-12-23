using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Requests;

[Serializable]
public class CreateMeeting : RequestBase
{
    public User Admin { get; set; } = null!;
    public Meeting Meeting { get; set; } = null!;
}
