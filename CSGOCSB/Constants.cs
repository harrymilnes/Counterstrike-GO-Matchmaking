using System.Windows.Media;

namespace CSGOCSB
{
    public static class Constants
    {
        public static readonly SolidColorBrush PingingButtonBrushColour = new SolidColorBrush(Colors.White);
        public static readonly SolidColorBrush BlockedServerPingIndicator = new SolidColorBrush(Colors.Transparent);
        public static readonly SolidColorBrush BlockedButtonBorder = (SolidColorBrush)new BrushConverter().ConvertFrom("#C62828");
        public static readonly SolidColorBrush BlockButtonPendingBorder = (SolidColorBrush)new BrushConverter().ConvertFrom("#1565C0");
        public static readonly SolidColorBrush UnblockedButtonBorder = (SolidColorBrush)new BrushConverter().ConvertFrom("#2E7D32");
        public static readonly SolidColorBrush LowPingBrushColour = (SolidColorBrush)new BrushConverter().ConvertFrom("#558B2F");
        public static readonly SolidColorBrush MediumPingBrushColour = (SolidColorBrush)new BrushConverter().ConvertFrom("#F9A825");
        public static readonly SolidColorBrush HighPingBrushColour = (SolidColorBrush)new BrushConverter().ConvertFrom("#EF6C00");
        public static readonly SolidColorBrush VeryHighPingBrushColour = (SolidColorBrush)new BrushConverter().ConvertFrom("#D84315");
        public static readonly SolidColorBrush BottomMenuButtonBrushColour = (SolidColorBrush)new BrushConverter().ConvertFrom("#1565C0");

        public static string FirewallBlockCommand(string country, string remoteIpRange) => $"netsh advfirewall firewall add rule name=\"{country}\" dir=in action=block protocol=any enable=yes remoteip={remoteIpRange}";
        public static string FirewallUnblockCommand(string country) => $"netsh advfirewall firewall delete rule name={country}";

        public static readonly string TimeoutPhrase = "Timeout";
        public static readonly string PingPhrase = "Ping: ";
        public static readonly string PingingPhrase = "Pinging";
        public static readonly string BlockedPhrase = "Blocked";
        public static readonly string UnblockPhrase = "Unblock";
        public static readonly string BlockPhrase = "Block";
    }
}