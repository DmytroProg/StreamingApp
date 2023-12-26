namespace StreamingApp.BLL.Models;

[Serializable]
public class FileMessage : MessageBase
{
    public string OriginalFileName { get; set; } = null!;
    public string UniqueFileName { get; set; } = null!;
    public byte[] Data { get; set; } = null!;
}
