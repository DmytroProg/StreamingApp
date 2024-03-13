using StreamingApp.BLL.Interfaces;
using StreamingApp.Networking.Configs;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Runtime.Serialization;
using StreamingApp.BLL.Responses;
using StreamingApp.BLL.Requests;

namespace StreamingApp.Networking.Network
{
    public class TcpClientUser : ITcpClient
    {
        private TcpClient? _tcpClient;
        private NetworkStream? _networkStream;
        private readonly IFormatter _formatter;
        public event Action<ResponseBase>? Received;
        private readonly ILogger _logger;

        public TcpClientUser(ILogger logger)
        {
            _formatter = new BinaryFormatter();
            _logger = logger;   
        }

        public async Task SendRequestAsync(RequestBase request)
        {
            try
            {
                _networkStream = _tcpClient?.GetStream();
                using var ms = new MemoryStream();

                _formatter.Serialize(ms, request);
                await _networkStream.WriteAsync(ms.ToArray(), 0, (int)ms.Length);
                await _networkStream.FlushAsync();
            }
            catch (Exception ex) { 
                _logger.LogError(ex);
            }
        }

        public async Task ConnectAsync(IConfig config)
        {
            try
            {
                _tcpClient = new TcpClient();
                if (config is TcpConfig tcpConfig)
                {
                    await _tcpClient.ConnectAsync(tcpConfig.IPAddress, tcpConfig.Port);
                    _networkStream = _tcpClient.GetStream();
                }

                Thread responseThread = new(ReceiveResponses)
                {
                    IsBackground = true,
                };
                responseThread.Start();
            }
            catch (Exception ex) { _logger.LogError(ex); }
        }

        private void ReceiveResponses()
        {
            try
            {
                StreamReader? streamReader = null;
                while (true)
                {
                    streamReader = new StreamReader(_networkStream, Encoding.UTF8);
                    if (!_networkStream.DataAvailable) continue;
                    var response = (ResponseBase)_formatter.Deserialize(streamReader.BaseStream);
                    Received?.Invoke(response);
                }
            }
            catch (Exception ex){ _logger.LogError(ex); }
        }
    }
}
