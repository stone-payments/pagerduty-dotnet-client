using System.Runtime.Serialization;

namespace StoneCo.PagerDuty.Client.Contracts
{
    internal enum EventAction
    {
        [EnumMember(Value = "trigger")] 
        Trigger,
    }
}
