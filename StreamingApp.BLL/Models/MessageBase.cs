using System.ComponentModel.DataAnnotations.Schema;

namespace StreamingApp.BLL.Models;

[Serializable]
public abstract class MessageBase
{
    public int Id { get; set; }
    public int SenderId { get; set; }

    [NotMapped]
    public string SenderName { get; set; }
    public int ReceiverId { get; set; }
    public int MeetingId { get; set; }
    public DateTime CreatedAt { get; set; }
}
