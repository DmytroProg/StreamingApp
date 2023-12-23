namespace StreamingApp.BLL.Requests;

[Serializable]
public class LoadFileRequest : RequestBase
{
    public string UniqueFileName { get; set; } = null!;
    public string OriginalFileName { get; set; } = null!;
}
