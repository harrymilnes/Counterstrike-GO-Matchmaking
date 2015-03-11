using CSGOCSB.Helpers;
using System.Diagnostics;
using System.Windows;

namespace CSGOCSB
{
    public partial class MainWindow : Window
    {
        public static bool ApplicationClosing;

        public MainWindow()
        {
            CheckForMultipleInstances();

            if (!ApplicationClosing)
            {
                InitializeComponent();
                new SoftwareUpdateHelper().CheckIfRunningUnmovedVersion();
                new SoftwareUpdateHelper().CheckForNewerVersions();
            }
        }

        private void CheckVersion()
        {
            new SoftwareUpdateHelper().CheckForNewerVersions();
        }

        private void CheckForMultipleInstances()
        {
            if (Process.GetProcessesByName("CSGOCSB").Length > 1)
            {
                MessageBox.Show("Another instance of this program has been detected!");
                Application.Current.MainWindow.Close();
                ApplicationClosing = true;
            }
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ApplicationClosing = true;
            await new ServerClickHelper().ResetServerBlockingAsync();
        }
    }
}
