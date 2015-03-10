using System.Windows;
using CSGOMM.ViewModel;

namespace CSGOMM
{
    public partial class App : Application
    {
        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow window = new MainWindow();
            var viewModel = await MainWindowViewModel.MainWindowInitaliserAsync();
            window.DataContext = viewModel;
            window.Show();
        }
    }
}
