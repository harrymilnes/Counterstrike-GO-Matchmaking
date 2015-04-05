using CSGOCSB.DataAccess;
using CSGOCSB.HttpHelpers;
using CSGOCSB.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
            _serverRepository = serverRepository;
            this.AllServers = new ObservableCollection<ServerModel>(serverRepository.GetServers());
        }

        bool TopMenuCommandsCanExecute
        {
            get
            {
                return true;
            }
        }

        bool BottomMenuCommandsCanExecute
        {
            get
            {
                return true;
            }
        }

        #region Commands
        RelayCommand _AboutCommand;
        public ICommand AboutCommand
        {
            get
            {
                if (_AboutCommand == null)
                {
                    _AboutCommand = new RelayCommand(param => this.AboutCommandExecute(), param => this.TopMenuCommandsCanExecute);
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
                    _pingallCommand = new RelayCommand(param => this.PingAllCommandExecuteAsync(), param => this.TopMenuCommandsCanExecute);
                }
                return _pingallCommand;
            }
        }

        async Task PingAllCommandExecuteAsync()
        {
            if(ApplicationData.ApplicationStatus == ApplicationData.ProgramStatus.BlockingServers || ApplicationData.ApplicationStatus == ApplicationData.ProgramStatus.ObservingServerPings)
                await NetworkHelper.PingAllServersAsync();

            if (ApplicationData.ApplicationStatus == ApplicationData.ProgramStatus.ObservingServerLoad)
                await ServerLoadHelper.UpdateAllServerLoads();
        }

        RelayCommand _applyCommand;
        public ICommand ApplyCommand
        {
            get
            {
                if (_applyCommand == null)
                {
                    _applyCommand = new RelayCommand(param => this.ApplyServerBlockingCommandExecuteAsync(), param => this.BottomMenuCommandsCanExecute);
                }
                return _applyCommand;
            }
        }

        async Task ApplyServerBlockingCommandExecuteAsync()
        {
            ServerClickHelper.ApplyServerBlocking();
        }

        RelayCommand _resetCommand;
        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                {
                    _resetCommand = new RelayCommand(param => this.ResetAllCommandExecuteAsync(), param => this.BottomMenuCommandsCanExecute);
                }
                return _resetCommand;
            }
        }

        async Task ResetAllCommandExecuteAsync()
        {
            await ServerClickHelper.ResetServerBlockingAsync();
        }

        RelayCommand _blockServersMenuCommand;
        public ICommand BlockServersMenuCommand
        {
            get
            {
                if (_blockServersMenuCommand == null)
                {
                    _blockServersMenuCommand = new RelayCommand(param => this.BlockServersMenuCommandExecute(), param => this.TopMenuCommandsCanExecute);
                }
                return _blockServersMenuCommand;
            }
        }

        async Task BlockServersMenuCommandExecute()
        {
            if (ApplicationData.ApplicationStatus != ApplicationData.ProgramStatus.BlockingServers)
            {
                ApplicationData.ApplicationStatus = ApplicationData.ProgramStatus.BlockingServers;
                _blockServersButtonColour = new SolidColorBrush(Colors.Green);
                _serverLoadButtonColour = new SolidColorBrush(Colors.Red);
                _serverPingButtonColour = new SolidColorBrush(Colors.Red);
                BottomMenuButtonColour = new SolidColorBrush(Colors.DodgerBlue);
                BottomMenuButtonVisiblity = Visibility.Visible;
                ServerClickHelper.ChangeColourAllServer(new SolidColorBrush(Colors.White));
                ServerClickHelper.ChangeAllServerStatus("Pinging");
                await NetworkHelper.PingAllServersAsync();
            }
        }

        RelayCommand _serverLoadMenuCommand;
        public ICommand ServerLoadMenuCommand
        {
            get
            {
                if (_serverLoadMenuCommand == null)
                {
                    _serverLoadMenuCommand = new RelayCommand(param => this.ServerLoadMenuButtonExecute(), param => this.TopMenuCommandsCanExecute);
                }
                return _serverLoadMenuCommand;
            }
        }

        async Task ServerLoadMenuButtonExecute()
        {
            if (ApplicationData.ApplicationStatus != ApplicationData.ProgramStatus.ObservingServerLoad)
            {
                ApplicationData.ApplicationStatus = ApplicationData.ProgramStatus.ObservingServerLoad;
                _blockServersButtonColour = new SolidColorBrush(Colors.Red);
                _serverLoadButtonColour = new SolidColorBrush(Colors.Green);
                _serverPingButtonColour = new SolidColorBrush(Colors.Red);
                BottomMenuButtonColour = new SolidColorBrush(Colors.White);
                BottomMenuButtonVisiblity = Visibility.Hidden;
                ServerClickHelper.ChangeColourAllServer(new SolidColorBrush(Colors.White));
                await ServerLoadHelper.UpdateAllServerLoads();
            }
        }

        RelayCommand _serverPingMenuCommand;
        public ICommand ServerPingMenuCommand
        {
            get
            {
                if (_serverPingMenuCommand == null)
                {
                    _serverPingMenuCommand = new RelayCommand(param => this.ServerPingMenuButtonExecute(), param => this.TopMenuCommandsCanExecute);
                }
                return _serverPingMenuCommand;
            }
        }

        async Task ServerPingMenuButtonExecute()
        {
            if (ApplicationData.ApplicationStatus != ApplicationData.ProgramStatus.ObservingServerPings)
            {
                ApplicationData.ApplicationStatus = ApplicationData.ProgramStatus.ObservingServerPings;
                _blockServersButtonColour = new SolidColorBrush(Colors.Red);
                _serverLoadButtonColour = new SolidColorBrush(Colors.Red);
                _serverPingButtonColour = new SolidColorBrush(Colors.Green);
                BottomMenuButtonColour = new SolidColorBrush(Colors.White);
                BottomMenuButtonVisiblity = Visibility.Hidden;
                ServerClickHelper.ChangeColourAllServer(new SolidColorBrush(Colors.White));
                ServerClickHelper.ChangeAllServerStatus("Pinging");
                await NetworkHelper.PingAllServersAsync();
            }
        }
        #endregion

        #region Properties
        private Thickness _bottomMenuMargin = new Thickness(200, 450, 0, 0);
        public Thickness BottomMenuMargin
        {
            get
            {
                return _bottomMenuMargin;
            }
            set
            {
                _bottomMenuMargin = value;
                OnPropertyChanged(() => this.BottomMenuMargin);
            }
        }

        private Brush _bottomMenuButtonColour = new SolidColorBrush(Colors.DodgerBlue);
        public Brush BottomMenuButtonColour
        {
            get
            {
                return _bottomMenuButtonColour;
            }
            set
            {
                _bottomMenuButtonColour = value;
                OnPropertyChanged(() => this.BottomMenuButtonColour);
            }
        }

        private Visibility _bottomMenuButtonVisiblity = Visibility.Visible;
        public Visibility BottomMenuButtonVisiblity
        {
            get
            {
                return _bottomMenuButtonVisiblity;
            }
            set
            {
                _bottomMenuButtonVisiblity = value;
                OnPropertyChanged(() => this.BottomMenuButtonVisiblity);
            }
        }

        private Brush _blockServersButtonColour = new SolidColorBrush(Colors.Green);
        public Brush BlockServersButtonColour
        {
            get
            {
                return _blockServersButtonColour;
            }
            set
            {
                _blockServersButtonColour = value;
                OnPropertyChanged(() => this.BlockServersButtonColour);
            }
        }

        private Brush _serverLoadButtonColour = new SolidColorBrush(Colors.Red);
        public Brush ServerLoadButtonColour
        {
            get
            {
                return _serverLoadButtonColour;
            }
            set
            {
                _serverLoadButtonColour = value;
                OnPropertyChanged(() => this.ServerLoadButtonColour);
            }
        }

        private Brush _serverPingButtonColour = new SolidColorBrush(Colors.Red);
        public Brush ServerPingButtonColour
        {
            get
            {
                return _serverPingButtonColour;
            }
            set
            {
                _serverPingButtonColour = value;
                OnPropertyChanged(() => this.ServerPingButtonColour);
            }
        }
        #endregion
    }
}