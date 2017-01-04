using Livit.ABC.CommandStack.Events;
using Livit.ABC.Domain.Persistence;
using Livit.ABC.Domain.Scheduling;
using Livit.ABC.Infraestructure.Broker;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Framework.EventStore;
using Livit.ABC.Infraestructure.Mapper;

namespace Livit.ABC.CommandStack.Sagas
{
    public class ExternalProviderSaga : Saga,
        IStartWithMessage<ExternalProviderScheduleCreatedEvent>
    {
        private readonly IBus _bus = null;
        private readonly IEventStore _eventStore = null;
        private readonly ISchedulingRepository _schedulingRepository = null;
        public ExternalProviderSaga(IBus bus, IEventStore eventStore, ISchedulingRepository schedulingRepository) : base(bus, eventStore)
        {
            _bus = bus;
            _eventStore = eventStore;
            _schedulingRepository = schedulingRepository;

        }

        public void Handle(ExternalProviderScheduleCreatedEvent message)
        {
            var schedulingRequest = new SchedulingRequest();
            var evt = MapUtil.Map
                <ExternalProviderScheduleCreatedEvent,
                SchedulingRequestExternalScheduleProviderCreatedEvent>(message);
            schedulingRequest.Apply(evt);
            var response = _schedulingRepository.SetScheduleExternalProviderInformation(schedulingRequest);
            if(response.Success)
                _eventStore.Save(message);

        }

    }
}