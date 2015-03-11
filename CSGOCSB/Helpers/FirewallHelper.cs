using CSGOCSB.Model;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CSGOCSB.Helpers
{
    public class FirewallHelper
    {
        public async Task DecideToBlockOrUnblockAsync(ServerModel steamServer)
        {
            if (steamServer.BlockStatus == false)
            {
                BlockServer(steamServer);
            }
            else
            {
                await UnblockServerAsync(steamServer);
            }
        }

        public void BlockServer(ServerModel steamServer)
        {
            var steamServerCommandString = String.Format(@"netsh advfirewall firewall add rule name=""[CS:GO] {0} Competitive Server"" description=""[CS:GO] Blocking the competitive servers for {0}"" dir=out action=block protocol=any remoteip={1}", steamServer.Country, steamServer.RemoteIpRange);
            FirewallExecution(steamServerCommandString);
            steamServer.BlockStatus = true;
            steamServer.BlockedColourBrush = new SolidColorBrush(Colors.Red);
            steamServer.Ping = "Blocked";
        }

        public async Task UnblockServerAsync(ServerModel steamServer)
        {
            var steamServerCommandString = String.Format(@"netsh advfirewall firewall delete rule name=""[CS:GO] {0} Competitive Server""", steamServer.Country);
            FirewallExecution(steamServerCommandString);
            if (!MainWindow.ApplicationClosing)
            {
                steamServer.Ping = "Pinging";
                await new Network().PingAsync(steamServer);
            }
        }

        private void FirewallExecution(string firewallString)
        {
            var process = Process.Start(new ProcessStartInfo(Environment.GetEnvironmentVariable("windir") + "\\System32\\cmd.exe", "/c " + firewallString)
            {
                CreateNoWindow = true,
                UseShellExecute = false
            });
            process.WaitForExit();
            process.Close();
        }
    }
}
