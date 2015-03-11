using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace CSGOCSB.Helpers
{
    public class SoftwareUpdateHelper
    {
        public static string CurrentVersion = "0.1.1";
        public static string ApiVersion;

        public void CheckForNewerVersions()
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    string apiUrl = "http://csgocsb.milnes.org/version/";
                    var ApiVersion = webClient.DownloadString(apiUrl);
                    if (ApiVersion != CurrentVersion)
                    {
                        var promptBoxDescription = String.Format("Newer version of this software has been detected, would you like to update?\n\nCurrent version: {0}\nLive version: {1}", CurrentVersion, ApiVersion);
                        var promptBox = MessageBox.Show(promptBoxDescription, "Update Detected", MessageBoxButton.YesNo);
                        switch (promptBox)
                        {
                            case MessageBoxResult.Yes:
                                    GetLatestVersion();
                                break;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public void CheckIfRunningUnmovedVersion()
        {
            if (Assembly.GetExecutingAssembly().Location.Contains(Path.GetTempPath()))
            {
                Process[] process = Process.GetProcesses();
                foreach (var p in process)
                {
                    if (p.ProcessName.Contains("CSGOCSB") && p.Id != Process.GetCurrentProcess().Id)
                        p.Kill();
                }
                File.Copy(Assembly.GetExecutingAssembly().Location, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CSGOCSB.exe", true);
            }
        }

        private void GetLatestVersion()
        {
            try
            {
                if (CurrentVersion != ApiVersion)
                {
                    using (WebClient WebClient = new WebClient())
                    {
                        var userTemporaryPath = Path.GetTempPath();
                        WebClient.DownloadFile(new Uri("http://www.csgocsb.milnes.org/Downloads/CSGOCSB.exe"), userTemporaryPath + "\\CSGOCSB.exe");
                        Process.Start(userTemporaryPath + @"\\CSGOCSB.exe");
                    }
                }
            }
            catch
            {
            }
        }
    }
}
