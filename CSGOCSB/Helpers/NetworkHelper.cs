using CSGOCSB.HttpHelpers;
using CSGOCSB.Model;
using CSGOCSB.ViewModel;
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CSGOCSB
{
    public static class NetworkHelper
    {
        public static async Task PingAsync(ServerModel steamServer)
        {
            bool serverWasBlockedBeforePing = false;
            if (ApplicationData.ApplicationStatus == ApplicationData.ProgramStatus.ObservingServerPings && ApplicationData.CurrentBlockedServers.Contains(steamServer))
            {
                FirewallHelper.UnblockServerWithoutModelAlterations(steamServer);
                serverWasBlockedBeforePing = true;
            }
            try
            {
                ServerClickHelper.ChangeColourUnclickedServer(steamServer, new SolidColorBrush(Colors.White));
                var pingSender = new Ping().SendPingAsync(steamServer.Hostname, 5000);
                var results = await pingSender;
                if (results.Status == IPStatus.Success)
                {
                    if (ApplicationData.ApplicationStatus == ApplicationData.ProgramStatus.ObservingServerPings)
                    {
                        if (results.RoundtripTime >= 0 && results.RoundtripTime < 60)
                            ServerClickHelper.ChangeColourUnclickedServer(steamServer, new SolidColorBrush(Colors.Green));

                        if (results.RoundtripTime >= 60 && results.RoundtripTime < 90)
                            ServerClickHelper.ChangeColourUnclickedServer(steamServer, new SolidColorBrush(Colors.Yellow));

                        if (results.RoundtripTime >= 90 && results.RoundtripTime < 200)
                            ServerClickHelper.ChangeColourUnclickedServer(steamServer, new SolidColorBrush(Colors.Orange));

                        if (results.RoundtripTime >= 200)
                            ServerClickHelper.ChangeColourUnclickedServer(steamServer, new SolidColorBrush(Colors.Red));
                    }
                    else
                    {
                        ServerClickHelper.ChangeColourUnclickedServer(steamServer, new SolidColorBrush(Colors.Green));
                    }
                    steamServer.Ping = String.Format("Ping: {0}", results.RoundtripTime);
                }
                else if (results.Status.ToString() == "11050") // 11050 - Generic Failure.
                {
                    if (ApplicationData.ApplicationStatus == ApplicationData.ProgramStatus.BlockingServers)
                    {
                        ServerClickHelper.ChangeColourUnclickedServer(steamServer, new SolidColorBrush(Colors.Red));
                        steamServer.Ping = String.Format("Blocked");
                    }
                }
                else
                {
                    ServerClickHelper.ChangeColourUnclickedServer(steamServer, new SolidColorBrush(Colors.Red));
                    steamServer.Ping = String.Format("Timeout");                    
                }
            }
            catch
            {
                ServerClickHelper.ChangeColourUnclickedServer(steamServer, new SolidColorBrush(Colors.Red));
                steamServer.Ping = "Timeout";
            }
            if (serverWasBlockedBeforePing)
            {
                FirewallHelper.BlockServerWithoutModelAlterations(steamServer);
                steamServer.BlockStatus = true;
            }
            else
            {
                steamServer.BlockStatus = false;
            }
        }

        public static async Task PingAllServersAsync()
        {
            var servers = MainWindowViewModel.viewModel.AllServers;
            foreach (var server in servers)
            {
                var awaitedServer = server;
                await PingAsync(awaitedServer);
            }
        }

        public static async Task<bool> NetworkHasInternetAccess()
        {
            try
            {
                var pingSender = new Ping().SendPingAsync("milnes.org", 10000);
                var results = await pingSender;
                if (results.Status == IPStatus.Success)
                {
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }
    }
}