using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;
using StreamingApp.Networking.Configs;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Networking.Network
{
    public class TcpClientServer : ITcpServer
    {
        private TcpListener? _listener = null;
        private NetworkStream _networkStream;
        private IFormatter _formatter;
        public event Func<RequestBase, TcpClient, Task>? RequestReceived;
        private readonly ILogger _logger;

        public TcpClientServer(ILogger logger)
        {
            _formatter = new BinaryFormatter();
            _logger = logger;
        }

        public async Task SendResponseAsync(TcpClient client, ResponseBase response)
        {
            try
            {
                _networkStream = client.GetStream();
                MemoryStream stream = new MemoryStream();
                _formatter.Serialize(stream, response);
                byte[] buffer = stream.ToArray();
                await _networkStream.WriteAsync(buffer, 0, buffer.Length);
                await _networkStream.FlushAsync();
            }
            catch (Exception ex){
                _logger.LogError(ex);
            }
        }

        public async Task StartListenAsync(IConfig config)
        {
            try
            {
                if (config is IPConfig tcpConfig)
                {
                    _listener = new TcpListener(tcpConfig.IPAddress, tcpConfig.Port);
                    _listener.Start();
                }

                await WaitForClients();
            }
            catch (Exception ex){
                _logger.LogError(ex);
            }
        }

        private async Task WaitForClients()
        {
            while (true)
            {
                if (_listener is null)
                    throw new NullReferenceException(nameof(_listener));

                TcpClient client = await _listener.AcceptTcpClientAsync();

                var thread = new Thread(() => Listen(client))
                {
                    IsBackground = true
                };
                thread.Start();
            }
        }

        private void Listen(TcpClient client)
        {
            try
            {
                var network = client.GetStream();
                StreamReader? streamReader = null;

                while (true)
                {
                    streamReader = new StreamReader(network, Encoding.UTF8);
                    if (!network.DataAvailable) continue;
                    var request = (RequestBase)_formatter.Deserialize(streamReader.BaseStream);

                    RequestReceived?.Invoke(request, client);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex);
            }
        }

        public Task StartListenAsync(string ipAddress, int port)
        {
            var config = new IPConfig()
            {
                IPAddress = System.Net.IPAddress.Parse(ipAddress),
                Port = port
            };
            return StartListenAsync(config);
        }
    }
}
