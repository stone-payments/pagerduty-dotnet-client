# PagerDuty dotnet client

Simple PagerDuty client for dotnet.

## Events API v2
Enables you to add PagerDuty's advanced event and incident management functionality to any system that can make an outbound HTTP connection.

### Send Event

Usage:
```
var client = new PagerDutyClient("https://events.pagerduty.com", SERVICE_ROUTING_KEY, new HttpClient());

await client.TriggerCriticalEventAsync("My service", EventAction.Trigger, Severity.Error, "Something went wrong!");
```