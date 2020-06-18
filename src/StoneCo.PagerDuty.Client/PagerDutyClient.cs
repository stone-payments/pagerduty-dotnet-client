using Newtonsoft.Json;
using StoneCo.PagerDuty.Client.Contracts;
using StoneCo.PagerDuty.Client.Exception;
using StoneCo.PagerDuty.Client.Extension;
using StoneCo.PagerDuty.Client.Settings;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StoneCo.PagerDuty.Client
{
    public class PagerDutyClient : IPagerDutyClient
    {
        private const string SendEventEndpoint = "v2/enqueue";
        private readonly HttpClient _httpClient;

        public PagerDutyClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public PagerDutyClient(PagerDutySettings pagerDutySettings, HttpMessageHandler httpMessageHandler = null)
        {
            _httpClient = httpMessageHandler is null 
                ? new HttpClient() 
                : new HttpClient(httpMessageHandler);

            PagerDutyDependenceExtension.ConfigureHttpClient(_httpClient, pagerDutySettings);
        }

        private async Task Trigger(string source, string summary, EventAction action, Severity severity)
        {
            await SendEvent(new SendEventRequest(source, action, severity, summary));
        }

        private async Task SendEvent(SendEventRequest e)
        {
            HttpResponseMessage response = null;

            try
            {
                using var request = new StringContent(JsonConvert.SerializeObject(e));

                response = await _httpClient.PostAsync(SendEventEndpoint, request);

                response.EnsureSuccessStatusCode();
            }
            catch (System.Exception ex)
            {
                throw new PagerDutyTriggerException(ex
                    , response is null
                        ? "No content."
                        : $"Response {response.Content.ReadAsStringAsync()}.");
            }
        }

        public Task TriggerCriticalEventAsync(string source, string summary) =>
            Trigger(source, summary, EventAction.Trigger, Severity.Critical);

        public Task TriggerErrorEventAsync(string source, string summary) =>
            Trigger(source, summary, EventAction.Trigger, Severity.Error);

        public Task TriggerInfoEventAsync(string source, string summary) =>
            Trigger(source, summary, EventAction.Trigger, Severity.Info);

        public Task TriggerWarningEventAsync(string source, string summary) =>
            Trigger(source, summary, EventAction.Trigger, Severity.Warning);
    }
}
