namespace StoneCo.PagerDuty.Client.Exception
{
    internal class PagerDutyTriggerException : System.Exception
    {
        public PagerDutyTriggerException(System.Exception exception, string message) : base(message, exception)
        {
        }
    }
}
