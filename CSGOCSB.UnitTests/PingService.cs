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
    public class PingServiceTests
    {
        private NetworkService _networkService;
        private ServerModel _testServer;
        private Mock<IPingWrapper> _pingWrapper;

        [TestInitialize]
        public void Setup()
        {
            _pingWrapper = new Mock<IPingWrapper>();
            _networkService = new NetworkService();
            _testServer = new ServerModel
            {
                BlockStatus = false,
                Country = "United Kingdom",
                RemoteIpRange = "127.0.0.1",
                Hostname = "milnes.org"
            };
        }

        [TestMethod]
        public async Task Server_Shows_Correct_Ping()
        {
            await _networkService.Ping(_testServer);
            Assert.AreNotEqual(0, _testServer.Ping);
            Assert.AreNotEqual(9999, _testServer.Ping);
        }

        [TestMethod]
        public async Task Ping_And_Indiciator_Correct_For_Timeout()
        {
            _pingWrapper.Setup(x => x.Ping(_testServer.Hostname, 3000)).Returns(Task.FromResult((long)0));

            var networkService = new NetworkService(_pingWrapper.Object);
            await networkService.Ping(_testServer);

            Assert.AreEqual(TimeoutPhrase, _testServer.Ping);
            Assert.IsTrue(new SolidColorBrushComparer().Equals(UnblockedButtonBorder, (SolidColorBrush)_testServer.BlockedButtonBorderBrush));
            Assert.IsTrue(new SolidColorBrushComparer().Equals(PingingButtonBrushColour, (SolidColorBrush)_testServer.PingIndicatorBrushColour));
        }

        [TestMethod]
        public async Task Ping_Indiciator_Is_Correct_Brush_Colour_For_Low_Ping()
        {
            var testPing = 50;
            _pingWrapper.Setup(x => x.Ping(_testServer.Hostname, 3000)).Returns(Task.FromResult((long)testPing));

            var networkService = new NetworkService(_pingWrapper.Object);
            await networkService.Ping(_testServer);

            Assert.AreEqual($"{PingPhrase} {testPing}", _testServer.Ping);
            Assert.IsTrue(new SolidColorBrushComparer().Equals(LowPingBrushColour, (SolidColorBrush)_testServer.PingIndicatorBrushColour));
        }

        [TestMethod]
        public async Task Ping_Indiciator_Is_Correct_Brush_Colour_For_Medium_Ping()
        {
            var testPing = 70;
            _pingWrapper.Setup(x => x.Ping(_testServer.Hostname, 3000)).Returns(Task.FromResult((long)testPing));

            var networkService = new NetworkService(_pingWrapper.Object);
            await networkService.Ping(_testServer);

            Assert.AreEqual($"{PingPhrase} {testPing}", _testServer.Ping);
            Assert.IsTrue(new SolidColorBrushComparer().Equals(MediumPingBrushColour, (SolidColorBrush)_testServer.PingIndicatorBrushColour));
        }

        [TestMethod]
        public async Task Ping_Indiciator_Is_Correct_Brush_Colour_For_High_Ping()
        {
            var testPing = 140;
            _pingWrapper.Setup(x => x.Ping(_testServer.Hostname, 3000)).Returns(Task.FromResult((long)testPing));

            var networkService = new NetworkService(_pingWrapper.Object);
            await networkService.Ping(_testServer);

            Assert.AreEqual($"{PingPhrase} {testPing}", _testServer.Ping);
            Assert.IsTrue(new SolidColorBrushComparer().Equals(HighPingBrushColour, (SolidColorBrush)_testServer.PingIndicatorBrushColour));
        }

        [TestMethod]
        public async Task Ping_Indiciator_Is_Correct_Brush_Colour_For_Very_High_Ping()
        {
            var testPing = 250;
            _pingWrapper.Setup(x => x.Ping(_testServer.Hostname, 3000)).Returns(Task.FromResult((long)testPing));

            var networkService = new NetworkService(_pingWrapper.Object);
            await networkService.Ping(_testServer);

            Assert.AreEqual($"{PingPhrase} {testPing}", _testServer.Ping);
            Assert.IsTrue(new SolidColorBrushComparer().Equals(VeryHighPingBrushColour, (SolidColorBrush)_testServer.PingIndicatorBrushColour));
        }

        [TestMethod]
        public async Task Block_Button_Uses_Correct_Brush_Colour_When_Blocked()
        {
            _testServer.BlockStatus = true;

            var networkService = new NetworkService(_pingWrapper.Object);
            await networkService.Ping(_testServer);

            Assert.IsTrue(new SolidColorBrushComparer().Equals(BlockedButtonBorder, (SolidColorBrush)_testServer.BlockedButtonBorderBrush));
            Assert.IsTrue(new SolidColorBrushComparer().Equals(BlockedServerPingIndicator, (SolidColorBrush)_testServer.PingIndicatorBrushColour));
        }

        [TestMethod]
        public async Task Block_Button_Uses_Correct_Brush_Colour_When_Unblocked()
        {
            _testServer.BlockStatus = false;

            var networkService = new NetworkService(_pingWrapper.Object);
            await networkService.Ping(_testServer);

            Assert.IsTrue(new SolidColorBrushComparer().Equals(UnblockedButtonBorder, (SolidColorBrush)_testServer.BlockedButtonBorderBrush));
        }

        [TestMethod]
        public async Task Blocked_Server_Is_Still_Blocked_After_Pinging()
        {
            _testServer.BlockStatus = true;

            await _networkService.Ping(_testServer);
            Assert.IsTrue(_testServer.BlockStatus);
        }

        [TestMethod]
        public async Task Unblocked_Server_Is_Still_Unblocked_After_Pinging()
        {
            _testServer.BlockStatus = false;

            var networkService = new NetworkService(_pingWrapper.Object);
            await _networkService.Ping(_testServer);
            Assert.IsFalse(_testServer.BlockStatus);
        }
    }
}