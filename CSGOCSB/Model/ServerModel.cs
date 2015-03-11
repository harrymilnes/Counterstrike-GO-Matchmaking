using CSGOCSB.Helpers;
using CSGOCSB.ViewModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace CSGOCSB.Model
{
    public class Margin
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }

        public Margin(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}", this.Left, this.Top, this.Right, this.Bottom);
        }
    }

    public class ServerModel : ViewModelBase
    {
        public string Country { get; set; }
        public string Hostname { get; set; }
        public string RemoteIpRange { get; set; }
        public Margin ButtonMargin { get; set; }
        public string ButtonMarginString { get; set; }
        public Margin LabelMargin { get; set; }
        public string LabelMarginString { get; set; }

        public static async Task<ServerModel> CreateServerAsync(string country, Margin margin, string hostname, string remoteIpRange)
        {
            var steamServer = new ServerModel
            {
                Country = country,
                Hostname = hostname,
                RemoteIpRange = remoteIpRange,
                ButtonMargin = margin,
                ButtonMarginString = margin.ToString(),
                LabelMargin = new Margin(margin.Left + 5, margin.Top + 20, margin.Right, margin.Bottom),
                Ping = "Pinging",
                BlockedColourBrush = new SolidColorBrush(Colors.White)
            };

            steamServer.LabelMarginString = steamServer.LabelMargin.ToString();
            await new Network().PingAsync(steamServer);
            return steamServer;
        }

        RelayCommand _ServerSelectCommand;
        public ICommand ServerSelectCommand
        {
            get
            {
                if (_ServerSelectCommand == null)
                {
                    _ServerSelectCommand = new RelayCommand(param => this.SelectCommandExecuteAsync(), param => this.BlockCommandCanExecute);
                }
                return _ServerSelectCommand;
            }
        }

        public void SelectCommandExecuteAsync()
        {
            new ServerClickHelper().ServerClickedAsync(this);
        }

        bool BlockCommandCanExecute
        {
            get
            {
                return true;
            }
        }

        private Brush _blockedColourBrush;
        public Brush BlockedColourBrush
        {
            get
            {
                return _blockedColourBrush;
            }
            set
            {
                _blockedColourBrush = value;
                OnPropertyChanged(() => this.BlockedColourBrush);
            }
        }
        private bool _blockStatus;
        public bool BlockStatus
        {
            get
            {

                return _blockStatus;
            }
            set
            {
                _blockStatus = value;
                OnPropertyChanged(() => this.BlockStatus);
            }
        }

        private string _ping;
        public string Ping
        {
            get
            {
                return _ping;
            }
            set
            {
                _ping = value;
                OnPropertyChanged(() => this.Ping);
            }
        }
    }
}
