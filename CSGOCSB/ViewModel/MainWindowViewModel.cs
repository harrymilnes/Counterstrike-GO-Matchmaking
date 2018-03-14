using CSGOCSB.DataAccess;
using System.Collections.ObjectModel;

namespace CSGOCSB.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<ViewModelBase> _viewModels;
        public static ServerListViewModel viewModel;

        private MainWindowViewModel(ViewModelBase viewModel)
        {
            this.ViewModels.Add(viewModel);
        }

        public static MainWindowViewModel MainWindowInitaliserAsync()
        {
            viewModel = new ServerListViewModel(new ServerRepository());
            return new MainWindowViewModel(viewModel);
        }

        public ObservableCollection<ViewModelBase> ViewModels
        {
            get => _viewModels ?? (_viewModels = new ObservableCollection<ViewModelBase>());
        }
    }
}