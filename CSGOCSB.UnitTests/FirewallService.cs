using CSGOCSB.Model;
using CSGOCSB.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Windows.Media;
using static CSGOCSB.Constants;

namespace CSGOCSB.UnitTests
{
    [TestClass]
    public class FirewallServiceTest
    {
        private ServerModel _testServer;
        private FirewallService _firewallService;
        private NetworkService _networkService;
        private Mock<IPingWrapper> _pingWrapper;

        [TestInitialize]
        public void Setup()
        {
            _pingWrapper = new Mock<IPingWrapper>();
            _firewallService = new FirewallService();
            _networkService = new NetworkService();

            _testServer = new ServerModel
            {
                BlockStatus = false,
                Country = "United Kingdom",
                Hostname = "milnes.org",
                RemoteIpRange = "192.30.253.112"
            };
        }

        [TestMethod]
        public void Server_Status_Updated_On_Block()
        {
            _firewallService.BlockServer(_testServer);

            Assert.IsTrue(_testServer.BlockStatus);
        }

        [TestMethod]
        public async Task Server_Status_Updated_On_Unblock()
        {
            _testServer.BlockStatus = true;
            await _firewallService.UnblockServer(_testServer);

            Assert.IsFalse(_testServer.BlockStatus);
        }

        [TestMethod]
        public async Task Toggle_Blocks_Unblocked_Server()
        {
            _testServer.BlockStatus = false;
            await _firewallService.ToggleServerBlock(_testServer);

            Assert.IsTrue(_testServer.BlockStatus);
        }

        [TestMethod]
        public async Task Toggle_Unblocks_Blocked_Server()
        {
            _testServer.BlockStatus = true;
            await _firewallService.ToggleServerBlock(_testServer);

            Assert.IsFalse(_testServer.BlockStatus);
        }

        [TestMethod]
        public void Blocked_Server_Is_Correct_Colour()
        {
            _firewallService.BlockServer(_testServer);

            Assert.IsTrue(new SolidColorBrushComparer().Equals(BlockedButtonBorder, (SolidColorBrush)_testServer.BlockedButtonBorderBrush));
            Assert.IsTrue(new SolidColorBrushComparer().Equals(BlockedServerPingIndicator, (SolidColorBrush)_testServer.PingIndicatorBrushColour));
        }

        [TestMethod]
        public async Task Ping_Handles_Blocked_Servers()
        {
            _pingWrapper.Setup(x => x.Ping(_testServer.Hostname, 3000)).Returns(Task.FromResult((long)250));

            var networkService = new NetworkService(_pingWrapper.Object);
            _testServer.BlockStatus = true;
            await networkService.Ping(_testServer);

            Assert.AreEqual(BlockedPhrase, _testServer.Ping);
            Assert.AreEqual(UnblockPhrase, _testServer.BlockButtonContent);
            Assert.IsTrue(new SolidColorBrushComparer().Equals(BlockedButtonBorder, (SolidColorBrush)_testServer.BlockedButtonBorderBrush));
            Assert.IsTrue(new SolidColorBrushComparer().Equals(BlockedServerPingIndicator, (SolidColorBrush)_testServer.PingIndicatorBrushColour));
        }

        [TestMethod]
        public async Task Ping_Responds_For_Unblocked_Server()
        {
            var testPing = 250;
            _pingWrapper.Setup(x => x.Ping(_testServer.Hostname, 3000)).Returns(Task.FromResult((long)testPing));

            var networkService = new NetworkService(_pingWrapper.Object);
            _testServer.BlockStatus = false;
            await networkService.Ping(_testServer);

            Assert.AreEqual(BlockPhrase, _testServer.BlockButtonContent);
            Assert.AreEqual($"{PingPhrase} {testPing}", _testServer.Ping);
        }
    }
}