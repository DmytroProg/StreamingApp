using GalaSoft.MvvmLight.Command;
using StreamingApp.BLL.Models;
using StreamingApp.WPF.ViewModels.Base;
using StreamingApp.WPF.ViewModels.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StreamingApp.WPF.ViewModels;

internal class ChatViewModel : MeetingViewModelBase
{
    private string _message;

    public ChatViewModel(Meeting meeting) : base(meeting)
    {
        SendMessageCommand = new RelayCommand(SendMessage);
        Messages = new ObservableCollection<MessageViewModel>();
        foreach(var message in meeting.Messages)
        {
            Messages.Add(new TextMessageViewModel(message as TextMessage, 
                message.SenderId == UserInfo.CurrentUser.Id));
        }
    }

    public ObservableCollection<MessageViewModel> Messages { get; set; }

    public string Message {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged();
        }
    }

    public ICommand SendMessageCommand { get; set; }

    private void SendMessage()
    {
        var message = new TextMessage()
        {
            MeetingId = Meeting.Id,
            ReceiverId = 0,
            Text = _message,
        };
        App.UnitController.MessageController.SendMessage(message);
        //Messages.Add(new TextMessageViewModel(message, true));
    }
}
