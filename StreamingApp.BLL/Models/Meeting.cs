namespace StreamingApp.BLL.Models;

[Serializable]
public class Meeting
{
    public int Id { get; set; }
    public string? MeetingCode { get; set; }
    public string Title { get; set; } = null!;
    public ICollection<User> Users { get; set; } = null!;
    public int AdminId { get; set; }
    public ICollection<MessageBase> Messages { get; set; } = null!;
}
