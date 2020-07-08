using System.Runtime.Serialization;

namespace StoneCo.PagerDuty.Client.Contracts
{
    public enum Severity
    {
        [EnumMember(Value = "critical")] 
        Critical,

        [EnumMember(Value = "error")] 
        Error,

        [EnumMember(Value = "warning")] 
        Warning,

        [EnumMember(Value = "info")] 
        Info,
    }
}
