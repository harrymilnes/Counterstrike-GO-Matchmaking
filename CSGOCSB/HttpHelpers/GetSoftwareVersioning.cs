using System.Net;

namespace CSGOCSB.HttpHelpers
{
    public static partial class HttpHelpers
    {
        public static string GetApiVersion()
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    return webClient.DownloadString(ApplicationData.ServerVersioningApiUrl);
                }
            }
            catch
            {
                return ApplicationData.CurrentVersion;
            }
        }
    }
}
