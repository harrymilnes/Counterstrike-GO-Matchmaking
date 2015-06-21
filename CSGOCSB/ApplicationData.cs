using CSGOCSB.Model;
using System.Collections.Generic;

namespace CSGOCSB
{
    public static class ApplicationData
    {
        public static string ServerLoadApiUrl = "http://www.api.milnes.org/CSGOServers/";

        public enum ProgramStatus
        {
            BlockingServers,
            ObservingServerLoad,
            ObservingServerPings
        }
        
        public static IList<ServerModel> ClickedServerList = new List<ServerModel>();
        public static IList<ServerModel> CurrentBlockedServers = new List<ServerModel>();
        public static ServerLoadModel ServerLoadList = new ServerLoadModel();
        public static ProgramStatus ApplicationStatus = ProgramStatus.BlockingServers;
        public static bool IsApplicationClosing;
    }
}
