using System.Runtime.Serialization;

namespace StoneCo.PagerDuty.Client.Contracts
{
    public enum EventAction
    {
        [EnumMember(Value = "acknowledge")] 
        Acknowledge,

        [EnumMember(Value = "resolve")] 
        Resolve,

        [EnumMember(Value = "trigger")] 
        Trigger,
    }
}
