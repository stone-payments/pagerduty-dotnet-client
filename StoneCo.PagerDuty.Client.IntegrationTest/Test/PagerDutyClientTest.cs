using Microsoft.Extensions.Options;
using StoneCo.PagerDuty.Client.IntegrationTest.Settings;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace StoneCo.PagerDuty.Client.IntegrationTest.Test
{
    public class PagerDutyClientTest : IntegratedTestBase
    {
        private readonly IPagerDutyClient _pagerDutyClient;

        public PagerDutyClientTest()
        {
            var options = Resolve<IOptions<PagerDutySettingsInTest>>();

            var pagerDutySettings = options.Value;

            _pagerDutyClient = new PagerDutyClient(pagerDutySettings.BaseAddress, pagerDutySettings.RoutingKey,
                new HttpClient());
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
    }
}
