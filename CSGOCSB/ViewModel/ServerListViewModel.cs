using System;
using System.Collections.ObjectModel;
using CSGOCSB.DataAccess;
using System.Windows.Input;
using CSGOCSB.Model;
using System.Diagnostics;
using CSGOCSB.Helpers;
using System.Threading.Tasks;

namespace CSGOCSB.ViewModel
{
    public class ServerListViewModel : ViewModelBase
    {
        ServerRepository _serverRepository;

        public static bool ServersCurrentlyBlocked;

        public ObservableCollection<ServerModel> AllServers
        {
            get;
            private set;
        }

        public ServerListViewModel(ServerRepository serverRepository)
        {
            if (serverRepository == null)
            {
                throw new ArgumentNullException("Server repository");
            }
            _serverRepository = serverRepository;
            this.AllServers = new ObservableCollection<ServerModel>(serverRepository.GetServers());
        }

        RelayCommand _AboutCommand;
        public ICommand AboutCommand
        {
            get
            {
                if (_AboutCommand == null)
                {
                    _AboutCommand = new RelayCommand(param => this.AboutCommandExecute(), param => this.CommandsCanExecute);
                }
                return _AboutCommand;
            }
        }

        public void AboutCommandExecute()
        {
            Process.Start("https://github.com/harrymilnes/CS-GO-CSB");
        }

        RelayCommand _pingallCommand;
        public ICommand PingAllCommand
        {
            get
            {
                if (_pingallCommand == null)
                {
                    _pingallCommand = new RelayCommand(param => this.PingAllCommandExecuteAsync(), param => this.CommandsCanExecute);
                }
                return _pingallCommand;
            }
        }

        async Task PingAllCommandExecuteAsync()
        {
            await new Network().PingAllServersAsync();
        }

        

        RelayCommand _applyCommand;
        public ICommand ApplyCommand
        {
            get
            {
                if (_applyCommand == null)
                {
                    _applyCommand = new RelayCommand(param => this.ApplyServerBlockingCommandExecuteAsync(), param => this.CommandsCanExecute);
                }
                return _applyCommand;
            }
        }

        async Task ApplyServerBlockingCommandExecuteAsync()
        {
            new ServerClickHelper().ApplyServerBlocking();
        }

        RelayCommand _resetCommand;
        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                {
                    _resetCommand = new RelayCommand(param => this.ResetAllCommandExecuteAsync(), param => this.CommandsCanExecute);
                }
                return _resetCommand;
            }
        }

        async Task ResetAllCommandExecuteAsync()
        {
            await new ServerClickHelper().ResetServerBlockingAsync();
        }

        bool CommandsCanExecute
        {
            get
            {
                return true;
            }
        }
    }
}
