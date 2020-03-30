using Newtonsoft.Json;
using StoneCo.PagerDuty.Client.Contracts;
using StoneCo.PagerDuty.Client.Exception;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoneCo.PagerDuty.Client
{
    public class PagerDutyClient : IPagerDutyClient
    {
        private const string SendEventEndpoint = "v2/enqueue";
        private readonly Uri _uri;
        private readonly HttpClient _httpClient;

        public PagerDutyClient(string baseAddress, string routingKey, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("x-routing-key", routingKey);
            _uri = new Uri(baseAddress);
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
                using var request = new HttpRequestMessage(HttpMethod.Post, new Uri(_uri, SendEventEndpoint))
                {
                    Content = new StringContent(JsonConvert.SerializeObject(e))
                };

                response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();
            }
            catch (System.Exception ex)
            {
                var content = response.Content.ReadAsStringAsync();
                throw new PagerDutyTriggerException(ex, $"Response {content}.");
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
