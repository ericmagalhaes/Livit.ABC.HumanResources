using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.CommandStack.Events
{
    /// <summary>
    /// created when a schedule is rejected by provider
    /// </summary>
    public class ExternalProviderScheduleRejectedEvent : Event
    {
        public ExternalProviderScheduleRejectedEvent(string requestId, string provider, string scheduleId)
        {
            RequestId = requestId;
            Provider = provider;
            ScheduleId = scheduleId;
        }
        public string RequestId { get; private set; }
        public string Provider { get; private set; }
        public string ScheduleId { get; private set; }
    }
}