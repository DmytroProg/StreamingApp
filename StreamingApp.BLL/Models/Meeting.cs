namespace StreamingApp.BLL.Models;

public class Meeting
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int UserId { get; set; }
    public List<User> Users { get; set; } = null!;
    public User Admin { get; set; } = null!;
    public List<MessageBase> Messages { get; set; } = null!;
}
