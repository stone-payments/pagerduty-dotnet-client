using Newtonsoft.Json;
using StoneCo.PagerDuty.Client.Contracts;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoneCo.PagerDuty.Client
{
    public class PagerDutyClient
    {
        private const string SEND_EVENT_ENDPOINT = "/v2/enqueue";
        private Uri _uri;
        private HttpClient _httpClient;

        public PagerDutyClient(string baseAddress, string routingKey)
        {
            _uri = new Uri(baseAddress);
            _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Add("x-routing-key", routingKey);
        }

        public async Task<SendEventResponse> Trigger(string source, EventAction action, Severity severity, string summary)
        {
            SendEventRequest e = new SendEventRequest(source, action, severity, summary);

            return await SendEvent(e);
        }

        private async Task<SendEventResponse> SendEvent(SendEventRequest e)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(_uri, SEND_EVENT_ENDPOINT));
            request.Content = new StringContent(JsonConvert.SerializeObject(e));

            var response = await _httpClient.SendAsync(request);

            return await HandleSendEventRespose(response);
        }

        private async Task<SendEventResponse> HandleSendEventRespose(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            SendEventResponse sendResponse;
            if (response.StatusCode == HttpStatusCode.Accepted ||
                response.StatusCode == HttpStatusCode.BadRequest)
            {
                sendResponse = JsonConvert.DeserializeObject<SendEventResponse>(content);
                sendResponse.StatusCode = (int)response.StatusCode;
                sendResponse.Success = response.StatusCode == HttpStatusCode.Accepted;
            }
            else if ((int)response.StatusCode == 429)
            {
                sendResponse = new SendEventResponse
                {
                    StatusCode = 429,
                    Success = false,
                    Message = "Too Many Requests. Easy, tiger!",
                };
            }
            else
            {
                throw new Exception($"Unmapped response with status code [{response.StatusCode}] received. Content: {content}");
            }

            return sendResponse;
        }
    }
}
