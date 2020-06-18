using StoneCo.PagerDuty.Client.Settings;
using System.Threading.Tasks;
using Xunit;

namespace StoneCo.PagerDuty.Client.IntegrationTest.Test
{
    public class PagerDutyClientTest : IntegratedTestBase
    {
        private readonly IPagerDutyClient _pagerDutyClient;
        private readonly PagerDutyClient _instancedPagerDutyClient;

        public PagerDutyClientTest()
        {
            _pagerDutyClient = Resolve<IPagerDutyClient>();

            _instancedPagerDutyClient = new PagerDutyClient(Resolve<PagerDutySettings>());
        }

        [Fact]
        public async Task PageDutyClientTest_TriggerCriticalEventAsync_Should_Execute_Successfully()
        {
            await _pagerDutyClient.TriggerCriticalEventAsync("PagerDuty.Client.IntegrationTest", "CriticalEvent");
        }

        [Fact]
        public async Task PageDutyClientTest_TriggerErrorEventAsync_Should_Execute_Successfully()
        {
            await _pagerDutyClient.TriggerErrorEventAsync("PagerDuty.Client.IntegrationTest", "ErrorEvent");
        }

        [Fact]
        public async Task PageDutyClientTest_TriggerInfoEventAsync_Should_Execute_Successfully()
        {
            await _pagerDutyClient.TriggerInfoEventAsync("PagerDuty.Client.IntegrationTest", "InfoEvent");
        }

        [Fact]
        public async Task PageDutyClientTest_TriggerWarningEventAsync_Should_Execute_Successfully()
        {
            await _pagerDutyClient.TriggerWarningEventAsync("PagerDuty.Client.IntegrationTest", "WarningEvent");
        }

        [Fact]
        public async Task InstancedPageDutyClientTest_TriggerCriticalEventAsync_Should_Execute_Successfully()
        {
            await _instancedPagerDutyClient.TriggerCriticalEventAsync("PagerDuty.Client.IntegrationTest", "CriticalEvent");
        }

        [Fact]
        public async Task InstancedPageDutyClientTest_TriggerErrorEventAsync_Should_Execute_Successfully()
        {
            await _instancedPagerDutyClient.TriggerErrorEventAsync("PagerDuty.Client.IntegrationTest", "ErrorEvent");
        }

        [Fact]
        public async Task InstancedPageDutyClientTest_TriggerInfoEventAsync_Should_Execute_Successfully()
        {
            await _instancedPagerDutyClient.TriggerInfoEventAsync("PagerDuty.Client.IntegrationTest", "InfoEvent");
        }

        [Fact]
        public async Task InstancedPageDutyClientTest_TriggerWarningEventAsync_Should_Execute_Successfully()
        {
            await _instancedPagerDutyClient.TriggerWarningEventAsync("PagerDuty.Client.IntegrationTest", "WarningEvent");
        }
    }
}
