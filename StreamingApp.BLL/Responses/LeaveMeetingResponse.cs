using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Responses;

[Serializable]
class LeaveMeetingResponse : ResponseBase
{
    public User LeftUser { get; set; } = null!;
}
