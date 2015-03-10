using CSGOCSB.Model;
using CSGOCSB.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CSGOCSB.Helpers
{

    public static class ServerClickHelper
    {
        public static IList<ServerModel> ClickedServerList = new List<ServerModel>();

        public static void ApplyServerBlocking()
        {
            if (ServerListViewModel.ServersCurrentlyBlocked)
                return;

            if (ClickedServerList.Count == 0)
                return;
           
            var SteamServers = MainWindowViewModel.viewModel.AllServers;
            var ServerstoBlock = SteamServers.Except(ClickedServerList).ToList();

            foreach (var unclickedServer in ServerstoBlock)
            {
                FirewallHelper.BlockServerAsync(unclickedServer);
            }

            foreach(var clickedServer in ClickedServerList)
            {
                clickedServer.BlockedColourBrush = new SolidColorBrush(Colors.DodgerBlue);
            }

            ServerListViewModel.ServersCurrentlyBlocked = true;
        }

        public static async Task ServerClicked(ServerModel clickedServer)
        {
            if (clickedServer.BlockStatus)
            {
                await FirewallHelper.UnblockServerAsync(clickedServer);
                clickedServer.BlockedColourBrush = new SolidColorBrush(Colors.DodgerBlue);
                return;
            }

            if(ClickedServerList.Contains(clickedServer))
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

        public static async Task ResetServerBlockingAsync()
        {
            var servers = MainWindowViewModel.viewModel.AllServers;
            ClickedServerList.Clear();

            foreach (var server in servers)
            {
                var awaitedServer = server;
                if (awaitedServer.BlockStatus)
                    await FirewallHelper.UnblockServerAsync(awaitedServer);
                
                if(!awaitedServer.BlockStatus)
                    awaitedServer.BlockedColourBrush = new SolidColorBrush(Colors.Green);
            }

            ServerListViewModel.ServersCurrentlyBlocked = false;

            if (MainWindow.ApplicationClosing)
                Application.Current.Shutdown();
        }

        public static void ChangeColourUnclickedServer(ServerModel steamServer, SolidColorBrush solidColourBrush)
        {
            if (steamServer.BlockedColourBrush.ToString() == "#FF1E90FF")
                return;

            steamServer.BlockedColourBrush = solidColourBrush;
        }
    }
}
