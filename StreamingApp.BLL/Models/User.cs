using System.Net.Sockets;

namespace StreamingApp.BLL.Models;

[Serializable]
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;

    public override string ToString()
    {
        return $"{Id}| name: {Name} login: {Login} pass: {Password}";
    }
}
