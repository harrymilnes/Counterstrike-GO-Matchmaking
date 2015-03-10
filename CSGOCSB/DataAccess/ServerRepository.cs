using System.Collections.Generic;
using CSGOMM.Model;
using System.Threading.Tasks;

namespace CSGOMM.DataAccess
{
    public class ServerRepository
    {
        readonly List<ServerModel> _servers;

        public ServerRepository()
        {
            if(_servers == null)
            {
                _servers = new List<ServerModel>();
            }
        }

        public async Task InitaliseAsync()
        {
            _servers.Add(await ServerModel.CreateServerAsync("USWest", new Margin(44, 143, 0, 0), "eat.valve.net", "192.69.96.0-192.69.96.255,192.69.97.0-192.69.97.255"));
            _servers.Add(await ServerModel.CreateServerAsync("USEast", new Margin(197, 163, 0, 0), "208.78.164.1", "208.78.164.0-208.78.164.255,208.78.165.0-208.78.165.255,208.78.166.0-208.78.166.255"));
            _servers.Add(await ServerModel.CreateServerAsync("Brazil", new Margin(330, 355, 0, 0), "gru.valve.net", "209.197.29.0-209.197.29.255,209.197.25.0-209.197.25.255,205.185.194.0-205.185.194.255"));
            _servers.Add(await ServerModel.CreateServerAsync("EUWest", new Margin(377, 168, 0, 0), "lux.valve.net", "146.66.152.0-146.66.152.255,146.66.158.0-146.66.158.255,146.66.159.0-146.66.159.255"));
            _servers.Add(await ServerModel.CreateServerAsync("Africa", new Margin(477, 334, 0, 0), "cpt-1.valve.net", "197.80.4.37,152.111.192.0-152.111.192.255,197.80.200.0-197.80.200.255,196.38.180.0-196.38.180.255"));
            _servers.Add(await ServerModel.CreateServerAsync("EUEast", new Margin(468, 183, 0, 0), "vie.valve.net", "146.66.155.0-146.66.155.255,185.25.182.0-185.25.182.255"));
            _servers.Add(await ServerModel.CreateServerAsync("Dubai", new Margin(527, 276, 0, 0), "dxb.valve.net", "185.25.183.0-185.25.183.255"));
            _servers.Add(await ServerModel.CreateServerAsync("Russia", new Margin(633, 148, 0, 0), "sto.valve.net", "146.66.156.0-146.66.156.255,146.66.157.0-146.66.157.255,185.25.180.0-185.25.180.255,185.25.181.0-185.25.181.255"));
            _servers.Add(await ServerModel.CreateServerAsync("India", new Margin(600, 293, 0, 0), "116.202.224.146", "180.149.41.0-180.149.41.255,116.202.224.146"));
            _servers.Add(await ServerModel.CreateServerAsync("SEAsia", new Margin(685, 268, 0, 0), "sgp-1.valve.net", "103.28.54.0-103.28.54.255,103.28.55.0-103.28.55.255,103.10.124.0-103.10.124.255"));
            _servers.Add(await ServerModel.CreateServerAsync("Australia", new Margin(714, 396, 0, 0), "syd.valve.net", "103.10.125.0-103.10.125.255"));
        }

        public List<ServerModel> GetServers()
        {
            return new List<ServerModel>(_servers);
        }

        public static async Task<ServerRepository> CreateAsync()
        {
            var sR = new ServerRepository();
            await sR.InitaliseAsync();
            return sR;
        }
    }
}
