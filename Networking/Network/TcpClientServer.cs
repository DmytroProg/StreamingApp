using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;
using StreamingApp.Networking.Configs;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

namespace Networking.Network
{
    public class TcpClientServer : ITcpServer
    {
        private TcpListener? _listener = null;
        private NetworkStream _networkStream;
        private IFormatter _formatter;
        public event Func<RequestBase, TcpClient, Task>? RequestReceived;

        public TcpClientServer()
        {
            _formatter = new BinaryFormatter();
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
                int a = 0;
            }
        }

        public async Task StartListenAsync(IConfig config)
        {
            if (_listener is not null)
            {
                throw new NullReferenceException(nameof(_listener));
            }

            try
            {
                if (config is TcpConfig tcpConfig)
                {
                    _listener = new TcpListener(tcpConfig.IPAddress, tcpConfig.Port);
                    _listener.Start();
                }

                await WaitForClients();
            }
            catch (Exception ex){
                int a = 0;
            }
        }

        private async Task WaitForClients()
        {
            while (true)
            {
                TcpClient client = await _listener.AcceptTcpClientAsync();

                await Task.Run(() => Listen(client));
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

            }
        }
    }
}
