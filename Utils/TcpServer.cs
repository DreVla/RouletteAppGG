using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RouletteApp.Model;

namespace RouletteApp.Utils
{
    public class TcpServer
    {
        private readonly int _port;
        private readonly Action<StatsMessage> _updateCallback;
        private TcpListener _tcpListener;
        private bool _isRunning;

        public TcpServer(int port, Action<StatsMessage> updateCallback)
        {
            _port = port;
            _updateCallback = updateCallback;
        }

        public void Start()
        {
            _tcpListener = new TcpListener(IPAddress.Any, _port);
            _tcpListener.Start();
            _isRunning = true;
            Debug.WriteLine($"TCP Listener started on port {_port}...");
            Task.Run(AcceptClientsAsync);
        }

        private async Task AcceptClientsAsync()
        {
            while (_isRunning)
            {
                try
                {
                    TcpClient client = await _tcpListener.AcceptTcpClientAsync();
                    _ = Task.Run(() => HandleClientAsync(client));
                    Debug.WriteLine("Client connected...");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"TCP Listener error: {ex.Message}");
                }
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                using (client)
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    var stats = JsonConvert.DeserializeObject<StatsMessage>(message);
                    if (stats != null)
                        _updateCallback?.Invoke(stats);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Client handling error: {ex.Message}");
            }
        }
    }
}
