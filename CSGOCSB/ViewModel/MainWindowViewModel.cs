using CSGOCSB.DataAccess;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CSGOCSB.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        ObservableCollection<ViewModelBase> _viewModels;
        public static ServerListViewModel viewModel;

        private MainWindowViewModel(ViewModelBase viewModel)
        {
            this.ViewModels.Add(viewModel);
        }

        public static async Task<MainWindowViewModel> MainWindowInitaliserAsync()
        {
            var __serverRepository = await ServerRepository.CreateAsync();
            viewModel = new ServerListViewModel(__serverRepository);
            return new MainWindowViewModel(viewModel);
        }

        public ObservableCollection<ViewModelBase> ViewModels
        {
            get
            {
                if(_viewModels == null)
                {
                    _viewModels = new ObservableCollection<ViewModelBase>();
                }
                return _viewModels;
            }
        }
    }
}
