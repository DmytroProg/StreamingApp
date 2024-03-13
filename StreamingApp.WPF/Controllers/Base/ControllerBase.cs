using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.Presenters;
using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;

namespace StreamingApp.WPF.Controllers.Base;

public abstract class ControllerBase
{
    private static ControllerBase _currentSender;
    protected readonly ILogger? _logger;
    protected readonly ITcpClient _tcpClient;
    protected readonly IPresenter _presenter;

    public ControllerBase(ITcpClient tcpClient, IPresenter presenter)
        : this(null, tcpClient, presenter)
    {
    }

    public ControllerBase(ILogger? logger, ITcpClient tcpClient, IPresenter presenter)
    {
        _logger = logger;
        _tcpClient = tcpClient;
        _presenter = presenter;
        _tcpClient.Received += _tcpClient_Received;
    }

    public virtual void SetSender()
    {
        _currentSender = this;
    }

    private void _tcpClient_Received(ResponseBase response)
    {
        if(response is StartSharingResponse startRes &&
            startRes.SenderId != UserInfo.CurrentUser.Id
            && _presenter is IScreenSharePresenter)
        {
            _presenter.ChangeView(response);
        }

        if(_currentSender == this)  
            _presenter.ChangeView(response);
    }
}