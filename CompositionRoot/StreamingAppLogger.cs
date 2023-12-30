using Microsoft.Extensions.Logging;
using System.Runtime.Versioning;

namespace CompositionRoot;

[SupportedOSPlatform("windows")]
internal class StreamingAppLogger : StreamingApp.BLL.Interfaces.ILogger
{
    private ILogger _logger;

    public StreamingAppLogger()
    {
        _logger = LoggerFactory.Create(builder => 
                        builder
                        .AddEventLog()
                        .SetMinimumLevel(LogLevel.Debug))
                        .CreateLogger("logger");
    }

    public void LogError(Exception ex)
    {
        _logger.LogError(ex.Message + Environment.NewLine + ex.StackTrace);
    }

    public void LogInfo(string message)
    {
        _logger.LogInformation(message);
    }
}
