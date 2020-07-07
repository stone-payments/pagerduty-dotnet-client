using System.Threading.Tasks;

namespace StoneCo.PagerDuty.Client
{
    public interface IPagerDutyClient
    {
        Task TriggerEventAsync(EventTriggerOptions options);
    }
}
