using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using RouletteApp.ViewModel;
using RouletteApp.Model;
using RouletteApp.Utils;

public class TcpServerTests
{
    private TcpServer _tcpServer;
    private StatsMessage _receivedStats;
    private const int TestPort = 5000;

    public TcpServerTests()
    {
        _tcpServer = new TcpServer(TestPort, stats => _receivedStats = stats);
        _tcpServer.Start();
    }

    [Fact]
    public async Task GIVEN_TcpClient_is_active_WHEN_data_is_being_sent_THEN_data_received_correctly()
    {
        var testStats = new StatsMessage { ActivePlayers = 5, BiggestMultiplier = 10 };
        string message = JsonConvert.SerializeObject(testStats);
        byte[] data = Encoding.UTF8.GetBytes(message);

        using (TcpClient client = new TcpClient())
        {
            await client.ConnectAsync("127.0.0.1", TestPort);
            using (NetworkStream stream = client.GetStream())
            {
                await stream.WriteAsync(data, 0, data.Length);
                await Task.Delay(100); // Allow some time for processing
            }
        }

        Assert.NotNull(_receivedStats);
        Assert.Equal(5, _receivedStats.ActivePlayers);
        Assert.Equal(10, _receivedStats.BiggestMultiplier);
    }
}
