using System.Threading.Tasks;

namespace StoneCo.PagerDuty.Client
{
    public interface IPagerDutyClient
    {
        Task TriggerCriticalEventAsync(string source, string summary);
        Task TriggerErrorEventAsync(string source, string summary);
        Task TriggerInfoEventAsync(string source, string summary);
        Task TriggerWarningEventAsync(string source, string summary);
    }
}
