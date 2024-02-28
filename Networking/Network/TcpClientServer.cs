using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Requests;
using StreamingApp.BLL.Responses;
using StreamingApp.Networking.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Networking.Network
{
    public class TcpClientServer : ITcpServer
    {
        private TcpListener? _listener = null;
        SemaphoreSlim semaphore;
        private IFormatter _formatter;
        public event Action<RequestBase, TcpClient>? RequestReceived;

        public TcpClientServer()
        {
            _formatter = new BinaryFormatter();
            semaphore = new SemaphoreSlim(5, 10);
        }

        public async Task SendResponseAsync(TcpClient client, ResponseBase response)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    await JsonSerializer.SerializeAsync(stream, response);
                    byte[] jsonBytes = stream.ToArray();
                    await client.GetStream().WriteAsync(jsonBytes, 0, jsonBytes.Length);
                }
            }
            catch (Exception ex){}
        }

        private async Task WaitForClient(IConfig config)
        {
            while (true)
            {
                await semaphore.WaitAsync();
                _ = Task.Run(() => StartListenAsync(config));
            }
        }

        public async Task StartListenAsync(IConfig config)
        {
            if (_listener is null)
            {
                semaphore.Release();
                throw new NullReferenceException(nameof(_listener));
            }
            try
            {
                TcpClient client = await _listener.AcceptTcpClientAsync();
                NetworkStream network = client.GetStream();
                StreamReader? streamReader = null;

                if (config is TcpConfig tcpConfig)
                {
                    _listener = new TcpListener(IPAddress.Any, tcpConfig.Port);
                    _listener.Start();
                }

                while (true)
                {
                    if (!network.DataAvailable) continue;
                    streamReader = new StreamReader(network, Encoding.UTF8);
                    var request = (RequestBase)_formatter.Deserialize(streamReader.BaseStream);

                    RequestReceived?.Invoke(request, client);

                }
            }
            catch (Exception ex){}
        }
    }
}
