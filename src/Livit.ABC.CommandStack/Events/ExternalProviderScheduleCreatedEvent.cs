using Livit.ABC.Domain.Scheduling;
using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.CommandStack.Events
{
    public class ExternalProviderScheduleCreatedEvent : Event
    {
        public ExternalProviderScheduleCreatedEvent(string requestId, string provider, string scheduleId)
        {
            RequestId = requestId;
            Provider = provider;
            ScheduleId = scheduleId;
        }
        public string RequestId { get; private set; }
        public string Provider { get; private set; }
        public string ScheduleId { get; private set; }

        public static class Factory
        {
            public static SchedulingRequestExternalScheduleProviderCreatedEvent Create(string requestId,string provider,string providerScheduleId)
            {
                var created = new SchedulingRequestExternalScheduleProviderCreatedEvent(requestId, provider,
                    providerScheduleId);
                return created;
            }
        }
    }
}