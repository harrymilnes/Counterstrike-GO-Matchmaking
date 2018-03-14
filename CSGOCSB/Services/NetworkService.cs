using CSGOCSB.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CSGOCSB.Constants;

namespace CSGOCSB.Services
{
    public class NetworkService
    {
        private readonly IPingWrapper _pingWrapper;
        public NetworkService(IPingWrapper pingWrapper)
        {
            _pingWrapper = pingWrapper;
        }

        public NetworkService()
        {
            _pingWrapper = new PingWrapper();
        }

        public async Task Ping(ServerModel server)
        {
            server.Ping = PingingPhrase;
            server.BlockedButtonBorderBrush = PingingButtonBrushColour;
            server.BlockButtonContent = BlockPhrase;
            server.PingIndicatorBrushColour = PingingButtonBrushColour;

            if (server.BlockStatus)
            {
                server.Ping = BlockedPhrase;
                server.BlockedButtonBorderBrush = BlockedButtonBorder;
                server.BlockButtonContent = UnblockPhrase;
                server.PingIndicatorBrushColour = BlockedServerPingIndicator;
                return;
            }

            try
            {
                UpdatePing(server, await _pingWrapper.Ping(server.Hostname, 3000));
            }
            catch
            {
                server.Ping = TimeoutPhrase;
                server.PingIndicatorBrushColour = PingingButtonBrushColour;
            }
        }

        public async Task PingAllServers(IEnumerable<ServerModel> servers)
        {
            foreach (var server in servers)
            {
                await Ping(server);
            }
        }

        private void UpdatePing(ServerModel server, long ping)
        {
            server.BlockedButtonBorderBrush = UnblockedButtonBorder;

            if (ping == 0)
            {
                server.PingIndicatorBrushColour = PingingButtonBrushColour;
                server.Ping = TimeoutPhrase;
                return;
            }

            server.Ping = $"{PingPhrase} {ping}";

            if (ping >= 0 && ping < 60)
                server.PingIndicatorBrushColour = LowPingBrushColour;

            if (ping >= 60 && ping < 90)
                server.PingIndicatorBrushColour = MediumPingBrushColour;

            if (ping >= 90 && ping < 200)
                server.PingIndicatorBrushColour = HighPingBrushColour;

            if (ping >= 200)
                server.PingIndicatorBrushColour = VeryHighPingBrushColour;
        }
    }
}