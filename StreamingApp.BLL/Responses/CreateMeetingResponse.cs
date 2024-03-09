using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Responses;

[Serializable]
public class CreateMeetingResponse : ResponseBase
{
    public Meeting Meeting { get; set; } = null!;
}
