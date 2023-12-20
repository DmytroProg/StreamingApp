using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Responses;

namespace StreamingApp.BLL.Controllers.Base;

public abstract class ControllerBase
{
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

    private void _tcpClient_Received(ResponseBase response)
    {
        _presenter.ChangeView(response);
    }
}
