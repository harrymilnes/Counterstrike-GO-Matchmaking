using CSGOCSB.HttpHelpers;
using System.Diagnostics;
using System.Windows;
using System.Linq;

namespace CSGOCSB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            CheckForMultipleInstances();

            if (!ApplicationData.IsApplicationClosing)
                InitializeComponent();
        }

        private void CheckForMultipleInstances()
        {
            if (Process.GetProcessesByName("CSGOBlocker").Count() > 1)
            {
                MessageBox.Show("Another instance of this program has been detected!");
                ApplicationData.IsApplicationClosing = true;
                Application.Current.MainWindow.Close();
            }
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ApplicationData.CurrentBlockedServers.Any())
            {
                ApplicationData.IsApplicationClosing = true;
                e.Cancel = true;
                await new ModelClickHelper().ResetServerBlockingAsync();
            }
        }
    }
}