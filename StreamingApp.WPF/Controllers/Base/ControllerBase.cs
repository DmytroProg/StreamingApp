using StreamingApp.BLL.Interfaces;

namespace StreamingApp.WPF.Controllers.Base;

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
    }
}