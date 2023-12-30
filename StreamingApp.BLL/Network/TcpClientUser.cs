using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.BLL.Network
{
    public class TcpClientUser : ITcpClient
    {
        private TcpClient? _tcpClient = null;
        private NetworkStream? _networkStream = null;
        private readonly BinaryFormatter _formatter;
        public event Action<ResponseBase>? Received;

        public TcpClientUser()
        {
            _formatter = new BinaryFormatter();
        }

        public async Task<ResponseBase> SendRequestAsync(RequestBase request)
        {
            if (_tcpClient is null || _networkStream is null)
                throw new ArgumentNullException(nameof(_networkStream));
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    _formatter.Serialize(ms, request);
                    await _networkStream?.WriteAsync(ms.ToArray(), 0, (int)ms.Length);
                    await _networkStream?.FlushAsync();
                    var response = _formatter.Deserialize(_networkStream) as ResponseBase;
                    return response!;
                }
            }
            catch (Exception ex) { return new ErrorResponse { ErrorMessage = ex.Message }; }
        }

        public async Task ConnectAsync(IConfig config)
        {
            if (_tcpClient is null || _networkStream is null)
                throw new ArgumentNullException(nameof(_networkStream));
            try
            {
                _tcpClient = new TcpClient();
                if(config is TcpConfig tcpConfig)
                    await _tcpClient.ConnectAsync(tcpConfig.IPAddress, tcpConfig.Port);
                _networkStream = _tcpClient.GetStream();
            }
            catch (Exception ex) {}
        }

        private void ReceiveResponses()
        {
            if (_tcpClient is null || _networkStream is null)
                throw new ArgumentNullException(nameof(_networkStream));
            try
            {
                var streamReader = new StreamReader(_networkStream, Encoding.UTF8);

                while (true)
                {
                    var response = (ResponseBase)_formatter.Deserialize(streamReader.BaseStream);
                    Received?.Invoke(response);
                }
            }
            catch (Exception ex){}
        }
    }
}
