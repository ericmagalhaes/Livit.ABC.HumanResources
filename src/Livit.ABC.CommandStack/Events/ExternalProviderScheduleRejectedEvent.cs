using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.CommandStack.Events
{
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