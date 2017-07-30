using CSGOCSB.Model;
using System.Collections.Generic;

namespace CSGOCSB
{
    public static class ApplicationData
    {
        public enum ProgramStatus
        {
            BlockingServers,
            ObservingServerPings
        }
        
        public static IList<ServerModel> ClickedServerList = new List<ServerModel>();
        public static IList<ServerModel> CurrentBlockedServers = new List<ServerModel>();
        public static ProgramStatus ApplicationStatus = ProgramStatus.BlockingServers;
        public static bool IsApplicationClosing;
    }
}
