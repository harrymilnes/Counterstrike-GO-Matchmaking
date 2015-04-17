using System;
using System.Diagnostics;
using System.Net;

namespace CSGOCSB.HttpHelpers
{
    public static partial class HttpHelpers
    {
        public static void GetLatestSoftwareExecutable(string userTemporaryPath)
        {
            try
            {
                var apiVersion = HttpHelpers.GetApiVersion();
                if (ApplicationData.CurrentVersion != apiVersion)
                {
                    using (WebClient WebClient = new WebClient())
                    {
                        WebClient.DownloadFile(new Uri("http://csgomatchmaker.com/Download?filename=CSGOBlocker.exe"), userTemporaryPath + "\\CSGOBlocker.exe");
                        Process.Start(userTemporaryPath + @"\\CSGOBlocker.exe");
                    }
                }
            }
            catch
            {
            }
        }
    }
}
