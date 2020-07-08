namespace StoneCo.PagerDuty.Client.Settings
{
    public class PagerDutySettings
    {
        public string? BaseAddress { get; set; } = "https://events.pagerduty.com";
        public string? RoutingKey { get; set; }
    }
}
