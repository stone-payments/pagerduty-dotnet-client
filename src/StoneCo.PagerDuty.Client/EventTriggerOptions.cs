using StoneCo.PagerDuty.Client.Contracts;

namespace StoneCo.PagerDuty.Client
{
    public class EventTriggerOptions
    {
        public EventTriggerOptions(Severity severity, string source, string summary)
        {
            Source = source;
            Summary = summary;
            Severity = severity;
        }

        public Severity Severity { get; }
        public string Source { get; set; }
        public string Summary { get; set; }
        public string? DedupKey { get; set; }
    }
}