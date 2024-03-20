using StreamingApp.BLL.Models;

namespace StreamingApp.WPF.ViewModels.Messages;

class TextMessageViewModel : MessageViewModel
{
    private string _text;

    public TextMessageViewModel(TextMessage message, bool isMe) : base(message, isMe)
    {
        Text = message.Text;
    }

    public string Text {
        get => _text;
        set
        {
            _text = value;
            OnPropertyChanged();
        }
    }
}
