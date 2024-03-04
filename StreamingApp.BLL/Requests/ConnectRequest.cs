using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Requests;

[Serializable]
public class ConnectRequest : RequestBase
{
    public int MeetingCode { get; set; }
    public User User { get; set; } = null!;
}
