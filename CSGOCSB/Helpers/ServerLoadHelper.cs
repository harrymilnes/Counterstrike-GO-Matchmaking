using CSGOCSB.ViewModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using CSGOCSB.HttpHelpers;

namespace CSGOCSB.HttpHelpers
{
    public static class ServerLoadHelper
    {
        public static async Task UpdateServerLoadData()
        {
            var serverLoadData = HttpHelpers.GetServerLoadData();
            ApplicationData.ServerLoadList = await serverLoadData;
        }

        public static async Task UpdateAllServerLoads()
        {
            var networkHasConnection = await NetworkHelper.NetworkHasInternetAccess();
            if(!networkHasConnection)
            {
                ServerClickHelper.ChangeColourAllServer(new SolidColorBrush(Colors.Red));
                ServerClickHelper.ChangeAllServerStatus("Timeout");
                return;
            }

            ServerClickHelper.ChangeColourAllServer(new SolidColorBrush(Colors.White));
            ServerClickHelper.ChangeAllServerStatus("Pinging");
            await UpdateServerLoadData();
            var servers = MainWindowViewModel.viewModel.AllServers;
            
            foreach (var server in servers)
            {
                var serverLoadData = (from d in ApplicationData.ServerLoadList.loadData where d.Servername == server.Country select new { d.Capacity, d.Load }).FirstOrDefault();
                server.Ping = string.Format("{0}", serverLoadData.Load);

                if (serverLoadData.Load.ToLower() == "idle" || serverLoadData.Load.ToLower() == "low")
                {
                    server.BlockedColourBrush = new SolidColorBrush(Colors.Green);
                    server.Ping = string.Format("   {0}", serverLoadData.Load); //Spacing is a bit hacky but it'll do for now.
                }

                if (serverLoadData.Load.ToLower() == "medium")
                    server.BlockedColourBrush = new SolidColorBrush(Colors.Orange);

                if (serverLoadData.Load.ToLower() == "high" || serverLoadData.Load.ToLower() == "full")
                {
                    server.Ping = string.Format("   {0}", serverLoadData.Load);
                    server.BlockedColourBrush = new SolidColorBrush(Colors.Red); //Spacing is a bit hacky but it'll do for now.
                } 
            }
        }
    }
}