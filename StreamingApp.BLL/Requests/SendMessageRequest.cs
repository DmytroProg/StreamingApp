﻿using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Requests;

[Serializable]
public class SendMessageRequest : RequestBase
{
    public MessageBase Message { get; set; } = null!;
}
