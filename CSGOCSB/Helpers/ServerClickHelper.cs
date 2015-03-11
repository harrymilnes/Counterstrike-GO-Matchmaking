using CSGOCSB.Model;
using CSGOCSB.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CSGOCSB.Helpers
{
    public class ServerClickHelper
    {
        public static IList<ServerModel> ClickedServerList = new List<ServerModel>();

        public void ApplyServerBlocking()
        {
            if (ServerListViewModel.ServersCurrentlyBlocked)
                return;

            if (ClickedServerList.Count == 0)
                return;

            var SteamServers = MainWindowViewModel.viewModel.AllServers;
            var ServerstoBlock = SteamServers.Except(ClickedServerList).ToList();

            foreach (var unclickedServer in ServerstoBlock)
            {
                new FirewallHelper().BlockServer(unclickedServer);
            }

            foreach (var clickedServer in ClickedServerList)
            {
                clickedServer.BlockedColourBrush = new SolidColorBrush(Colors.Green);
            }

            ClickedServerList.Clear();
            ServerListViewModel.ServersCurrentlyBlocked = true;
        }

        public async Task ServerClickedAsync(ServerModel clickedServer)
        {
            if (clickedServer.BlockStatus)
            {
                await new FirewallHelper().UnblockServerAsync(clickedServer);
                if (!ServerListViewModel.ServersCurrentlyBlocked)
                {
                    clickedServer.BlockedColourBrush = new SolidColorBrush(Colors.DodgerBlue);
                }
                return;
            }

            if (!ServerListViewModel.ServersCurrentlyBlocked)
            {
                if (ClickedServerList.Contains(clickedServer))
                {
                    ClickedServerList.Remove(clickedServer);
                    clickedServer.BlockedColourBrush = new SolidColorBrush(Colors.Green);
                    await new Network().PingAsync(clickedServer);
                }
                else
                {
                    clickedServer.BlockedColourBrush = new SolidColorBrush(Colors.DodgerBlue);
                    ClickedServerList.Add(clickedServer);
                }
            }
            else
            {
                if (!clickedServer.BlockStatus)
                {
                    new FirewallHelper().BlockServer(clickedServer);
                }
            }
        }

        public async Task ResetServerBlockingAsync()
        {
            var servers = MainWindowViewModel.viewModel.AllServers;
            ClickedServerList.Clear();

            foreach (var server in servers)
            {
                var awaitedServer = server;
                if (awaitedServer.BlockStatus)
                    await new FirewallHelper().UnblockServerAsync(awaitedServer);

                if (!awaitedServer.BlockStatus)
                    awaitedServer.BlockedColourBrush = new SolidColorBrush(Colors.Green);
            }

            ServerListViewModel.ServersCurrentlyBlocked = false;

            if (MainWindow.ApplicationClosing)
                Application.Current.Shutdown();
        }

        public void ChangeColourUnclickedServer(ServerModel steamServer, SolidColorBrush solidColourBrush)
        {
            if (steamServer.BlockedColourBrush.ToString() == "#FF1E90FF")
                return;

            steamServer.BlockedColourBrush = solidColourBrush;
        }
    }
}
