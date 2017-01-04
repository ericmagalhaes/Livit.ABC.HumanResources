using Livit.ABC.Infraestructure.Common;

namespace Livit.ABC.Domain.Scheduling
{
    public class SchedulingRequestExternalScheduleProviderCreatedEvent : DomainEvent
    {
        public SchedulingRequestExternalScheduleProviderCreatedEvent(string requestId, string provider, string providerScheduleId)
        {
            RequestId = requestId;
            Provider = provider;
            ProviderScheduleId = providerScheduleId;
        }

        public string RequestId { get; private set; }
        public string Provider { get; private set; }
        public string ProviderScheduleId { get; private set; }

    }
}