using CSGOCSB.Model;
using System.Collections.Generic;

namespace CSGOCSB.DataAccess
{
    public class ServerRepository
    {
        private IList<ServerModel> _servers;

        public ServerRepository()
        {
            _servers = new List<ServerModel>
            {
                ServerModel.CreateServer("Middle East - Dubai", "185.25.183.50", "185.25.183.0-185.25.183.255"),
                ServerModel.CreateServer("EU North - Russia/Stockholm", "146.66.156.50", "146.66.156.0-146.66.156.255,146.66.157.0-146.66.157.255,185.25.180.0-185.25.180.255,185.25.181.0-185.25.181.255"),
                ServerModel.CreateServer("EU East - Vienna", "146.66.155.1", "146.66.155.0-146.66.155.255,185.25.182.0-185.25.182.255"),
                ServerModel.CreateServer("EU West - Luxembourg", "146.66.152.1", "146.66.152.0-146.66.152.255,146.66.158.0-146.66.158.255,146.66.159.0-146.66.159.255"),
                ServerModel.CreateServer("SE Asia - Singapore", "103.28.54.1", "103.28.54.0-103.28.54.255,103.28.55.0-103.28.55.255,103.10.124.0-103.10.124.255,10.156.7.0-10.156.7.255,45.121.184.0-45.121.184.255,45.121.185.0-45.121.185.255"),
                ServerModel.CreateServer("South Africa - Cape town", "197.80.4.37", "197.80.4.37,152.111.192.0-152.111.192.255,197.80.200.0-197.80.200.255,196.38.180.0-196.38.180.255,197.84.209.0-197.84.209.255"),
                ServerModel.CreateServer("South America - Brazil/SaoPaulo", "209.197.29.50", "209.197.29.0-209.197.29.255,209.197.25.0-209.197.25.255,205.185.194.0-205.185.194.255"),
                ServerModel.CreateServer("India - New Delhi ", "119.82.77.186", "116.202.224.146,180.149.41.0-180.149.41.255,10.130.205.0-10.130.205.255"),
                ServerModel.CreateServer("Australia - Sydney ", "61.8.0.113", "103.10.125.0-103.10.125.255"),
                ServerModel.CreateServer("US Southwest - California", "162.254.194.1", "162.254.194.0-162.254.194.255"),
                ServerModel.CreateServer("US Southeast - Atlanta", "162.254.199.1", "162.254.199.0-162.254.199.255"),
                ServerModel.CreateServer("US East - Sterling/Virginia", "208.78.164.1", "208.78.164.0-208.78.164.255,208.78.165.0-208.78.165.255,208.78.166.0-208.78.166.255"),
                ServerModel.CreateServer("US West - Seattle/Washington", "192.69.96.1", "192.69.96.0-192.69.96.255,192.69.97.0-192.69.97.255"),
                ServerModel.CreateServer("Poland", "155.133.243.1", "155.133.243.0-155.133.243.255,155.133.242.0-155.133.242.255,155.133.240.0-155.133.240.255,155.133.241.0-155.133.241.255"),
                ServerModel.CreateServer("Japan", "61.14.157.158", "61.14.157.158,45.121.186.0-45.121.186.255,45.121.187.0-45.121.187.255")
            };
        }

        public IList<ServerModel> GetServers()
            => _servers;
    }
}