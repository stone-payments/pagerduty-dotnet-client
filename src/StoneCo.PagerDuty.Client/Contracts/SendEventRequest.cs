using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StoneCo.PagerDuty.Client.Contracts
{
    public class SendEventRequest
    {
        public SendEventRequest(Payload payload, EventAction eventAction)
        {
            Payload = payload;
            EventAction = eventAction;
        }

        public SendEventRequest(string source, EventAction eventAction, Severity severity, string summary)
            : this(new Payload(source, summary, severity), eventAction)
        { }

        [JsonProperty("event_action")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EventAction EventAction { get; set; }

        [JsonProperty("payload")]
        public Payload Payload { get; set; }
    }
}
