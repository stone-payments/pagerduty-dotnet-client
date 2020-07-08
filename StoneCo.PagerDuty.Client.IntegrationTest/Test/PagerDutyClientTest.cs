using StoneCo.PagerDuty.Client.Contracts;
using System.Threading.Tasks;
using Xunit;

namespace StoneCo.PagerDuty.Client.IntegrationTest.Test
{
    public class PagerDutyClientTest : IntegratedTestBase
    {
        private readonly IPagerDutyClient _pagerDutyClient;

        public PagerDutyClientTest()
        {
            _pagerDutyClient = Resolve<IPagerDutyClient>();
        }

        [Theory]
        [InlineData(Severity.Critical)]
        [InlineData(Severity.Error)]
        [InlineData(Severity.Info)]
        [InlineData(Severity.Warning)]
        public async Task GivenSeverity_ShouldTriggerSuccessfully(Severity severity)
        {
            const string source = "PagerDuty.Client.IntegrationTest";
            const string summary = "TestEvent";

            await _pagerDutyClient.TriggerEventAsync(new EventTriggerOptions(severity, source, summary));
        }
    }
}
