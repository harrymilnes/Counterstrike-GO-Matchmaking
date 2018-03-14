using CSGOCSB.DataAccess;
using CSGOCSB.HttpHelpers;
using CSGOCSB.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;

namespace CSGOCSB.ViewModel
{
    public class ServerListViewModel : ViewModelBase
    {
        private ModelClickHelper _modelClickHelper;
        private ServerRepository _serverRepository;

        public ObservableCollection<ServerModel> AllServers { get; private set; }

        public ServerListViewModel(ServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
            _modelClickHelper = new ModelClickHelper();
            this.AllServers = new ObservableCollection<ServerModel>(serverRepository.GetServers());
        }

        #region Commands
        RelayCommand _AboutCommand;
        public ICommand AboutCommand
        {
            get => _AboutCommand ?? (_AboutCommand = new RelayCommand(param => this.AboutCommandExecute(), param => true));
        }

        public void AboutCommandExecute()
            => _modelClickHelper.OpenAboutWebpage();

        RelayCommand _pingallCommand;
        public ICommand PingAllCommand
        {
            get => _pingallCommand ?? (_pingallCommand = new RelayCommand(param => this.PingAllCommandExecuteAsync(), param => true));
        }

        private async Task PingAllCommandExecuteAsync()
            => await _modelClickHelper.RefreshAllServers();

        RelayCommand _resetCommand;
        public ICommand ResetCommand
        {
            get => _resetCommand ?? (_resetCommand = new RelayCommand(param => this.ResetAllCommandExecuteAsync(), param => true));
        }

        private async Task ResetAllCommandExecuteAsync() 
            => await _modelClickHelper.ResetServerBlockingAsync();

        RelayCommand _serverPingMenuCommand;
        public ICommand ServerPingMenuCommand
        {
            get => _serverPingMenuCommand ?? (_serverPingMenuCommand = new RelayCommand(param => this.ServerPingMenuButtonExecute(), param => true));
        }

        private async Task ServerPingMenuButtonExecute()
        {
            if (ApplicationData.CurrentBlockedServers.Any())
            {
                _blockServersButtonColour = new SolidColorBrush(Colors.Red);
                _serverPingButtonColour = new SolidColorBrush(Colors.Green);
                await _modelClickHelper.RefreshAllServers();
            }
        }
        #endregion

        #region Properties
        private Brush _blockServersButtonColour = new SolidColorBrush(Colors.Green);
        public Brush BlockServersButtonColour
        {
            get => _blockServersButtonColour;

            set
            {
                _blockServersButtonColour = value;
                OnPropertyChanged(() => this.BlockServersButtonColour);
            }
        }

        private Brush _serverPingButtonColour = new SolidColorBrush(Colors.Red);
        public Brush ServerPingButtonColour
        {
            get => _serverPingButtonColour;

            set
            {
                _serverPingButtonColour = value;
                OnPropertyChanged(() => this.ServerPingButtonColour);
            }
        }
        #endregion
    }
}