using Newtonsoft.Json;

namespace StoneCo.PagerDuty.Client.Contracts
{
    public class SendEventResponse
    {
        [JsonIgnore]
        public int StatusCode { get; set; }
        
        [JsonIgnore]
        public bool Success { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("errors")]
        public string[] Errors { get; set; }

        [JsonProperty("dedup_key")]
        public string DedupKey { get; set; }
    }
}
