using CSGOCSB.Model;
using CSGOCSB.Services;
using CSGOCSB.ViewModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static CSGOCSB.ApplicationData;

namespace CSGOCSB.HttpHelpers
{
    public class ModelClickHelper
    {
        private readonly FirewallService _firewallService;
        private readonly NetworkService _networkService;

        public ModelClickHelper()
        {
            _firewallService = new FirewallService();
            _networkService = new NetworkService();
        }

        public async Task ServerClickedAsync(ServerModel clickedServer)
        {
            await _firewallService.ToggleServerBlock(clickedServer);
            await _networkService.Ping(clickedServer);
        }

        public void OpenAboutWebpage()
            => Process.Start("https://github.com/harrymilnes/CS-GO-CSB");

        public async Task RefreshAllServers()
            =>  await _networkService.PingAllServers(MainWindowViewModel.viewModel.AllServers);

        public async Task ResetServerBlockingAsync()
        {
            var servers = MainWindowViewModel.viewModel.AllServers
                .Where(x => CurrentBlockedServers.Any(c => c.Country == x.Country));

            foreach (var server in servers)
            {
                await _firewallService.UnblockServer(server, true);
            }

            if (IsApplicationClosing)
                Application.Current.Shutdown();
        }
    }
}