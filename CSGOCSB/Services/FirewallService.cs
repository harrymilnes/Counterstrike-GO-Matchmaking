using CSGOCSB.Model;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using static CSGOCSB.ApplicationData;

namespace CSGOCSB.Services
{
    public class FirewallService
    {
        private NetworkService _networkService;
        public FirewallService()
        {
            _networkService = new NetworkService();
        }

        public async Task ToggleServerBlock(ServerModel server, bool updateFeedback = true)
        {
            if (server.BlockStatus != true)
            {
                BlockServer(server, updateFeedback);
                return;
            }

            await UnblockServer(server, updateFeedback);
        }

        public void BlockServer(ServerModel server, bool updateFeedback = true)
        {
            ExecuteFirewallCommand(Constants.FirewallBlockCommand(server.Country, server.RemoteIpRange));

            server.BlockStatus = true;
            CurrentBlockedServers.Add(server);

            if (updateFeedback)
            {
                server.BlockedButtonBorderBrush = Constants.BlockedButtonBorder;
                server.BlockButtonContent = Constants.UnblockPhrase;
                server.PingIndicatorBrushColour = Constants.BlockedServerPingIndicator;
                server.Ping = Constants.BlockedPhrase;
            }
        }

        public async Task UnblockServer(ServerModel server, bool updateFeedback = true)
        {
            ExecuteFirewallCommand(Constants.FirewallUnblockCommand(server.Country));
            server.BlockStatus = false;
            CurrentBlockedServers.Remove(server);

            if(updateFeedback && !IsApplicationClosing)
            {
                await _networkService.Ping(server);
            }
        }

        private void ExecuteFirewallCommand(string command)
        {
            var cmdPromptProcess = Process.Start(new ProcessStartInfo(Environment.GetEnvironmentVariable("windir") + "\\System32\\cmd.exe", "/c " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = false
            });

            cmdPromptProcess.WaitForExit();
            cmdPromptProcess.Close();
        }
    }
}