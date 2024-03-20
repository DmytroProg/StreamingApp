using StreamingApp.BLL.Models;
using StreamingApp.WPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.WPF.ViewModels.Messages;

class MessageViewModel : ViewModelBase
{
    public bool IsMe { get; }
    public bool IsNotMe => !IsMe;
    public string Sender { get; set; }
    public string SendTime { get; set; }

    public MessageViewModel(MessageBase message, bool isMe)
    {
        IsMe = isMe;
        Sender = isMe ? "me" : message.SenderName;
        SendTime = message.CreatedAt.ToShortTimeString();
    }
}
