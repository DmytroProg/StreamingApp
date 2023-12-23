namespace StreamingApp.BLL.Models;

[Serializable]
public class ImageMessage : MessageBase
{
    public byte[] ImageData { get; set; } = null!;
    public string FileName { get; set; } = null!;
} 
