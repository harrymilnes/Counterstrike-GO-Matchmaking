using System.Collections.Generic;

namespace CSGOCSB.Model
{
    public class ServerLoadData
    {
        public string Servername { get; set; }
        public string Capacity { get; set; }
        public string Load { get; set; }
    }

    public class ServerLoadModel
    {
        public List<ServerLoadData> loadData { get; set; }
    }
}
