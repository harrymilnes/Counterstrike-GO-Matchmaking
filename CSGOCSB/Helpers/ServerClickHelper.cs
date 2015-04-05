using CSGOCSB.Model;
using CSGOCSB.ViewModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CSGOCSB.HttpHelpers
{
    public static class ServerClickHelper
    {
        public static void ApplyServerBlocking()
        {
            if (ServerListViewModel.ServersCurrentlyBlocked)
                return;

            if (ApplicationData.ClickedServerList.Count == 0)
                return;

            var SteamServers = MainWindowViewModel.viewModel.AllServers;
            var ServerstoBlock = SteamServers.Except(ApplicationData.ClickedServerList).ToList();

            foreach (var unclickedServer in ServerstoBlock)
            {
                FirewallHelper.BlockServer(unclickedServer);
            }

            foreach (var clickedServer in ApplicationData.ClickedServerList)
            {
                clickedServer.BlockedColourBrush = new SolidColorBrush(Colors.Green);
            }

            ApplicationData.ClickedServerList.Clear();
            ServerListViewModel.ServersCurrentlyBlocked = true;
        }

        public static async Task ServerClickedAsync(ServerModel clickedServer)
        {
            if (ApplicationData.CurrentBlockedServers.Contains(clickedServer))
            {
                await FirewallHelper.UnblockServerAsync(clickedServer);
                if (!ServerListViewModel.ServersCurrentlyBlocked)
                {
                    clickedServer.BlockedColourBrush = new SolidColorBrush(Colors.DodgerBlue);
                }
                return;
            }

            if (!ServerListViewModel.ServersCurrentlyBlocked)
            {
                if (ApplicationData.ClickedServerList.Contains(clickedServer))
                {
                    ApplicationData.ClickedServerList.Remove(clickedServer);
                    clickedServer.BlockedColourBrush = new SolidColorBrush(Colors.Green);
                    await NetworkHelper.PingAsync(clickedServer);
                }
                else
                {
                    clickedServer.BlockedColourBrush = new SolidColorBrush(Colors.DodgerBlue);
                    ApplicationData.ClickedServerList.Add(clickedServer);
                }
            }
            else
            {
                if (!ApplicationData.CurrentBlockedServers.Contains(clickedServer))
                {
                    FirewallHelper.BlockServer(clickedServer);
                }
            }
        }

        public static async Task ResetServerBlockingAsync()
        {
            var servers = MainWindowViewModel.viewModel.AllServers;
            ApplicationData.ClickedServerList.Clear();

            foreach (var server in servers)
            {
                var awaitedServer = server;
                if (ApplicationData.CurrentBlockedServers.Contains(server))
                    await FirewallHelper.UnblockServerAsync(awaitedServer);

                if (!ApplicationData.CurrentBlockedServers.Contains(server))
                    awaitedServer.BlockedColourBrush = new SolidColorBrush(Colors.Green);
            }

            ServerListViewModel.ServersCurrentlyBlocked = false;
            if (ApplicationData.IsApplicationClosing)
                Application.Current.Shutdown();
        }

        public static void ChangeColourUnclickedServer(ServerModel steamServer, SolidColorBrush solidColourBrush)
        {
            if (steamServer.BlockedColourBrush.ToString() == "#FF1E90FF")
                return;

            steamServer.BlockedColourBrush = solidColourBrush;
        }

        public static void ChangeColourAllServer(SolidColorBrush solidColourBrush)
        {
            var servers = MainWindowViewModel.viewModel.AllServers;
            foreach(var server in servers)
            {
                server.BlockedColourBrush = solidColourBrush;
            }
        }
        
        public static void ChangeAllServerStatus(string status)
        {
            var servers = MainWindowViewModel.viewModel.AllServers;
            foreach(var server in servers)
            {
                server.Ping = status;
            }
        }
    }
}
