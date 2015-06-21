using CSGOCSB.HttpHelpers;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows;

namespace CSGOCSB.HttpHelpers
{
    public static class SoftwareUpdateHelper
    {
        public static void CheckForNewerVersions()
        {
            var apiVersion = HttpHelpers.GetApiVersion().Replace("\"", "");
            if(apiVersion != ApplicationData.CurrentVersion)
            {
                var promptBoxDescription = String.Format("Newer version of this software has been detected, would you like to update?\n\nCurrent version: {0}\nLive version: {1}", ApplicationData.CurrentVersion, apiVersion);
                var promptBox = MessageBox.Show(promptBoxDescription, "Update Detected", MessageBoxButton.YesNo);
                switch (promptBox)
                {
                    case MessageBoxResult.Yes:
                        GetLatestVersion();
                        break;
                }
            }
        }

        public static void CheckIfRunningUnmovedVersion()
        {
            if (Assembly.GetExecutingAssembly().Location.Contains(Path.GetTempPath()))
            {
                Process[] process = Process.GetProcesses();
                foreach (var p in process)
                {
                    if (p.ProcessName.Contains("CSGOBlocker") && p.Id != Process.GetCurrentProcess().Id)
                        p.Kill();
                }
                File.Copy(Assembly.GetExecutingAssembly().Location, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CSGOBlocker.exe", true);
            }
        }

        private static void GetLatestVersion()
        {
            var usersTemporaryStoragePath = Path.GetTempPath();
            HttpHelpers.GetLatestSoftwareExecutable(usersTemporaryStoragePath);
        }
    }
}
