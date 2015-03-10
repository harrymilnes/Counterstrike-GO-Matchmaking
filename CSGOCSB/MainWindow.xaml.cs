using CSGOCSB.Helpers;
using System.Windows;

namespace CSGOCSB
{
    public partial class MainWindow : Window
    {
        public static bool ApplicationClosing;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ApplicationClosing = true;
            await ServerClickHelper.ResetServerBlockingAsync();
        }
    }
}
