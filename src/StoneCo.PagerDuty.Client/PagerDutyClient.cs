using Newtonsoft.Json;
using StoneCo.PagerDuty.Client.Contracts;
using StoneCo.PagerDuty.Client.Exception;
using StoneCo.PagerDuty.Client.Extension;
using StoneCo.PagerDuty.Client.Settings;
using System;
using System.Net.Http;
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

        public PagerDutyClient(PagerDutySettings pagerDutySettings, HttpMessageHandler? httpMessageHandler = null)
        {
            _httpClient = httpMessageHandler is null 
                ? new HttpClient() 
                : new HttpClient(httpMessageHandler);

            PagerDutyDependenceExtension.ConfigureHttpClient(_httpClient, pagerDutySettings);
        }

        public Task TriggerEventAsync(EventTriggerOptions options)
        {
            if (options is null) throw new ArgumentNullException(nameof(options));

            return SendEventAsync(new SendEventRequest(
                options.Source, EventAction.Trigger, options.Severity, options.Summary, options.DedupKey));
        }

        private async Task SendEventAsync(SendEventRequest e)
        {
            HttpResponseMessage? response = null;

            try
            {
                using var request = new StringContent(JsonConvert.SerializeObject(e));

                response = await _httpClient.PostAsync(SendEventEndpoint, request);

                var content = await response.Content.ReadAsStringAsync();

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
    }
}
