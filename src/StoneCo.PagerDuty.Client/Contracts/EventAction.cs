using System.Runtime.Serialization;

namespace StoneCo.PagerDuty.Client.Contracts
{
    public enum EventAction
    {
        [EnumMember(Value = "trigger")] 
        Trigger,
    }
}
