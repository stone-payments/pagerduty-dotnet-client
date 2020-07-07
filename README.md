# PagerDuty dotnet client

Simple PagerDuty client for dotnet.

## Events API v2
Enables you to add PagerDuty's advanced event and incident management functionality to any system that can make an outbound HTTP connection.

### Send Event

Usage:
``` csharp
var settings = new PagerDutySettings 
{
	RoutingKey = SERVICE_ROUTING_KEY
};

var client = new PagerDutyClient(settings);

await client.TriggerEventAsync(new EventTriggerOptions(Severity.Error, "My service", "Something went wrong!");
```