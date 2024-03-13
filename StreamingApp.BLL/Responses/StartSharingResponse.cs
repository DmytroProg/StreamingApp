namespace StreamingApp.BLL.Responses;

[Serializable]
public class StartSharingResponse : ResponseBase
{
    public int SenderId { get; set; }
}
