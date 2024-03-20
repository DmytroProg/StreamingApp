using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.Presenters;
using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.ViewModels;
using StreamingApp.WPF.ViewModels.Base;
using StreamingApp.WPF.ViewModels.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.WPF.Presenters;

internal class MessagePresenter : IMessagePresenter
{
    private readonly ChatNavigationStore _navigationStore;
    public MessagePresenter(ChatNavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }
    public void ChangeView(ResponseBase response)
    {
        _navigationStore.ChatViewModel = response switch
        {
            MessageResponse messageResponse => OnMessageReceived(messageResponse),
            _ => new ErrorViewModel()
        };
    }

    private ChatViewModel OnMessageReceived(MessageResponse messageResponse)
    {
        return new ChatViewModel(messageResponse.Meeting);
    }
}
