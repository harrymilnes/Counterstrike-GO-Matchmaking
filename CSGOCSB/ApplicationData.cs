using CSGOCSB.Model;
using System.Collections.Generic;

namespace CSGOCSB
{
    public static class ApplicationData
    {
        public static IList<ServerModel> CurrentBlockedServers = new List<ServerModel>();
        public static bool IsApplicationClosing;
    }
}