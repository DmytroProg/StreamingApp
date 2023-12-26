namespace StreamingApp.BLL.Models;

[Serializable]
public class TextMessage : MessageBase
{
    public string Text { get; set; } = null!;
}
