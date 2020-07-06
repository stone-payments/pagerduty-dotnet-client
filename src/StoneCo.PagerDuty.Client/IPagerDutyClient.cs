using System.Threading.Tasks;

namespace StoneCo.PagerDuty.Client
{
    public interface IPagerDutyClient
    {
        Task TriggerCriticalEventAsync(string source, string summary, string dedupKey = null);
        Task TriggerErrorEventAsync(string source, string summary, string dedupKey = null);
        Task TriggerInfoEventAsync(string source, string summary, string dedupKey = null);
        Task TriggerWarningEventAsync(string source, string summary, string dedupKey = null);
    }
}
