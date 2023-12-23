namespace StreamingApp.BLL.Responses;

[Serializable]
public class LoadFileResponse : ResponseBase
{
    public string OriginalName { get; set; } = null!;
    public byte[] Data { get; set; } = null!;
}
