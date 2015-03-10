using CSGOMM.Helpers;
using CSGOMM.Model;
using CSGOMM.ViewModel;
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CSGOMM
{
    public class Network
    {
        public async Task PingAsync(ServerModel steamServer)
        {
            ServerClickHelper.ChangeColourUnclickedServer(steamServer, new SolidColorBrush(Colors.White));
            var pingSender = new Ping().SendPingAsync(steamServer.Hostname, 10000);
            var results = await pingSender;
            if (results.Status == IPStatus.Success)
            {
                ServerClickHelper.ChangeColourUnclickedServer(steamServer, new SolidColorBrush(Colors.Green));
                steamServer.BlockStatus = false;
                steamServer.Ping = String.Format("Ping: {0}", results.RoundtripTime);
                return;
            }
            else
            {
                ServerClickHelper.ChangeColourUnclickedServer(steamServer, new SolidColorBrush(Colors.Red));
                steamServer.BlockStatus = true;
                steamServer.Ping = String.Format("Blocked");
            }
        }

        public async Task PingAllServersAsync()
        {
            var servers = MainWindowViewModel.viewModel.AllServers;
            foreach (var server in servers )
            {
                var awaitedServer = server;
                if (awaitedServer.BlockStatus == false)
                    await PingAsync(awaitedServer);
            }
        }
    }
}

