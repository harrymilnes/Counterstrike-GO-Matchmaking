using CSGOCSB.HttpHelpers;
using CSGOCSB.Services;
using CSGOCSB.ViewModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace CSGOCSB.Model
{
    public class ServerModel : ViewModelBase
    {
        private readonly NetworkService _networkService;

        public string Country { get; set; }
        public string Hostname { get; set; }
        public string RemoteIpRange { get; set; }

        public ServerModel()
        {
            _networkService = new NetworkService();
        }

        public static ServerModel CreateServer(string country, string hostname, string remoteIpRange)
        {
            var steamServer = new ServerModel
            {
                Country = country,
                Hostname = hostname,
                RemoteIpRange = remoteIpRange,
                Ping = Constants.PingingPhrase,
                BlockedButtonBorderBrush = new SolidColorBrush(Colors.White),
                BlockButtonContent = Constants.BlockPhrase
            };

            new NetworkService().Ping(steamServer);

            return steamServer;
        }

        RelayCommand _ServerSelectCommand;
        public ICommand ServerSelectCommand
        {
            get => _ServerSelectCommand ?? (_ServerSelectCommand = new RelayCommand(param => this.SelectCommandExecuteAsync(), param => true));
        }

        public async Task SelectCommandExecuteAsync()
            => await new ModelClickHelper().ServerClickedAsync(this);

        private Brush _blockedButtonBorderBrush;
        public Brush BlockedButtonBorderBrush
        {
            get => _blockedButtonBorderBrush;

            set
            {
                _blockedButtonBorderBrush = value;
                OnPropertyChanged(() => this.BlockedButtonBorderBrush);
            }
        }

        private string _blockButtonContent;
        public string BlockButtonContent
        {
            get => _blockButtonContent;

            set
            {
                _blockButtonContent = value;
                OnPropertyChanged(() => this.BlockButtonContent);
            }
        }

        private Brush _pingIndicatorBrushColour;
        public Brush PingIndicatorBrushColour
        {
            get => _pingIndicatorBrushColour;

            set
            {
                _pingIndicatorBrushColour = value;
                OnPropertyChanged(() => this.PingIndicatorBrushColour);
            }
        }

        private bool _blockStatus;
        public bool BlockStatus
        {
            get => _blockStatus;

            set
            {
                _blockStatus = value;
                OnPropertyChanged(() => this.BlockStatus);
            }
        }

        private string _ping;
        public string Ping
        {
            get => _ping;

            set
            {
                _ping = value;
                OnPropertyChanged(() => this.Ping);
            }
        }
    }
}