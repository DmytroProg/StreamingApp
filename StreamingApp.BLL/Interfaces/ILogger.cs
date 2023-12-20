using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.BLL.Interfaces;

public interface ILogger
{
    void LogInfo(string message);
    void LogError(Exception ex);
}
