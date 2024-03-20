using StreamingApp.WPF.Controllers.Base;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Requests;
using System;
using System.IO;
using System.Threading.Tasks;
using StreamingApp.BLL.Interfaces.Presenters;
using StreamingApp.BLL.Responses;
using StreamingApp.WPF.ViewModels.Base;

namespace StreamingApp.WPF.Controllers;

public class MessageController : ControllerBase
{
    public MessageController(ILogger? logger, ITcpClient tcpClient, IMessagePresenter presenter)
        : base(logger, tcpClient, presenter)
    {
        
    }

    public ViewModelBase? ChatViewModel { get; set; }

    public async Task SendMessage(MessageBase message)
    {
        try
        {
            if (message == null) return;

            message.SenderId = UserInfo.CurrentUser.Id;
            message.SenderName = UserInfo.CurrentUser.Name;
            message.CreatedAt = DateTime.Now;

            var request = new SendMessageRequest()
            {
                Message = message,
                Sender = UserInfo.CurrentUser,
                MeetingId = UserInfo.Meeting.Id,
            };
            
            await _tcpClient.SendRequestAsync(request);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex);
        }
    }

    public FileMessage GetMessageFromFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath.Trim())) 
            throw new ArgumentNullException(nameof(filePath));
        try
        {
            string fileName = Path.GetFileName(filePath.Trim());
            byte[] fileData = File.ReadAllBytes(filePath.Trim());

            return new FileMessage()
            {
                OriginalFileName = fileName,
                Data = fileData,
                UniqueFileName = string.Empty,
            };
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex);
            return null;
        }
    }
}
