using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StoneCo.PagerDuty.Client.Contracts
{
    internal class Payload
    {
        public Payload(string source, string summary, Severity severity)
        {
            Source = source;
            Summary = summary;
            Severity = severity;
        }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("severity")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Severity Severity { get; set; }
    }
}
