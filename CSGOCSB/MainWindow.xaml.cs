using CSGOCSB.HttpHelpers;
using System.Diagnostics;
using System.Windows;

namespace CSGOCSB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            CheckForMultipleInstances();
            if (!ApplicationData.IsApplicationClosing)
            {
                InitializeComponent();
            }
        }

        private void CheckForMultipleInstances()
        {
            if (Process.GetProcessesByName("CSGOBlocker").Length > 1)
            {
                MessageBox.Show("Another instance of this program has been detected!");
                Application.Current.MainWindow.Close();
                ApplicationData.IsApplicationClosing = true;
            }
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ApplicationData.IsApplicationClosing = true;
            await ServerClickHelper.ResetServerBlockingAsync();
        }
    }
}
