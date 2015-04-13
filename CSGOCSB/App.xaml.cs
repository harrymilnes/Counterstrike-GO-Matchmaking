using System.Windows;
using CSGOCSB.ViewModel;

namespace CSGOCSB
{
    public partial class App : Application
    {
        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow window = new MainWindow();
            var viewModel = MainWindowViewModel.MainWindowInitaliserAsync();
            window.DataContext = viewModel;
            window.Show();
            await NetworkHelper.PingAllServersAsync();
        }
    }
}
