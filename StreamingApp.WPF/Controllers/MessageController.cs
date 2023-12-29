using StreamingApp.WPF.Controllers.Base;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;
using System;
using System.IO;
using System.Threading.Tasks;

namespace StreamingApp.WPF.Controllers;

public class MessageController : ControllerBase
{
    public MessageController(ITcpClient tcpClient, IPresenter presenter)
        : this(null, tcpClient, presenter)
    {
    }

    public MessageController(ILogger? logger, ITcpClient tcpClient, IPresenter presenter)
        : base(logger, tcpClient, presenter)
    {
    }

    public async Task SendMessage(MessageBase message, User user)
    {
        try
        {
            if (message is TextMessage textMessage) 
                textMessage.Text = "Some text";
            if (message == null) return;
            message.SenderId = user.Id;
            message.CreatedAt = DateTime.Now;
            var response = await _tcpClient.SendRequestAsync(
                new SendMessageRequest { Message = message });
            _presenter.ChangeView(response);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex);
        }
    }

    public async Task SendFileAsync(string filePath, User user)
    {
        try
        {
            if (string.IsNullOrEmpty(filePath)) return;
            string fileName = Path.GetFileName(filePath.Trim());
            byte[] fileData = File.ReadAllBytes(filePath.Trim());

            var fileRequest = new LoadFileRequest
            {
                UniqueFileName = fileName, 
                OriginalFileName = fileName
            };

            ResponseBase response = null;
            Action<ResponseBase> handler = (r) =>
            {
                response = r;
            };

            _tcpClient.Received += handler;
            await _tcpClient.SendFileAsync(fileRequest, fileData);

            while (response == null){await Task.Delay(100);}

            _tcpClient.Received -= handler;
            _presenter.ChangeView(response);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex);
        }
    }
}
