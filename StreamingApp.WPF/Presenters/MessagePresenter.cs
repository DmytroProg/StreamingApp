using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.Presenters;
using StreamingApp.BLL.Responses;
using StreamingApp.WPF.Navigations;
using StreamingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.WPF.Presenters;

internal class MessagePresenter : IMessagePresenter
{
    private readonly NavigationStore _navigationStore;
    public MessagePresenter(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }
    public void ChangeView(ResponseBase response)
    {
        _navigationStore.CurrectViewModel = response switch
        {
            _ => new ErrorViewModel()
        };
    }
}
