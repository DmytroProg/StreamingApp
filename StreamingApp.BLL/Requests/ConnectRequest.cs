using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Requests;

[Serializable]
public class ConnectRequest : RequestBase
{
    public IConfig Config { get; set; } = null!;
    public User User { get; set; } = null!;
}
