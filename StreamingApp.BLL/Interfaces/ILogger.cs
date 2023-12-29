namespace StreamingApp.BLL.Interfaces;

public interface ILogger
{
    void LogInfo(string message);
    void LogError(Exception ex);
}
