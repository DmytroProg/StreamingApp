using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.Presenters;
using StreamingApp.BLL.Responses;
using System.Windows.Controls;
using System.Windows.Media;

namespace StreamingApp.WPF.Presenters;

internal class ScreenSharePresenter : IScreenSharePresenter
{
    public Image CurrentScreen { get; set; }
    public void ChangeView(ResponseBase response)
    {
        //if (CurrentScreen == null) return;
        //if(response is SendScreenResponse screenResponse)
        //{
        //    CurrentScreen.Source = (ImageSource?)_converter.ConvertFrom(
        //        screenResponse.Screen);
        //}
    }
}
