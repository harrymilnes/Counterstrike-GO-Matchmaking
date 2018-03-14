using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace CSGOCSB.Services
{
    public interface IPingWrapper
    {
        Task<long> Ping(string hostname, int maxWait);
    }

    public class PingWrapper : IPingWrapper
    {
        public async Task<long> Ping(string hostname, int maxWait = 3000)
        {
            var pingResult = await new Ping().SendPingAsync(hostname, maxWait);
            return pingResult.RoundtripTime;
        }
    }
}