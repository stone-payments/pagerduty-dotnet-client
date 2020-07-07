using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StoneCo.PagerDuty.Client.Contracts
{
    internal class SendEventRequest
    {
        public SendEventRequest(Payload payload, EventAction eventAction, string? dedupKey)
        {
            Payload = payload;
            EventAction = eventAction;
            DedupKey = dedupKey;
        }

        public SendEventRequest(string source, EventAction eventAction, Severity severity, string summary, string? dedupKey)
            : this(new Payload(source, summary, severity), eventAction, dedupKey)
        { }

        [JsonProperty("event_action")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EventAction EventAction { get; set; }

        [JsonProperty("payload")]
        public Payload Payload { get; set; }

        [JsonProperty("dedup_key")]
        public string? DedupKey { get; set; }
    }
}
