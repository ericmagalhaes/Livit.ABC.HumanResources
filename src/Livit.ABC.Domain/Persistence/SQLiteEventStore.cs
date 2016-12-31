using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livit.ABC.Domain.Scheduling;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Framework.EventStore;
using Newtonsoft.Json;

namespace Livit.ABC.Domain.Persistence
{
    public class SQLiteEventStore : IEventStore
    {
        private readonly EventSourcingRepository _eventSourcingRepository = new EventSourcingRepository();

        public SQLiteEventStore()
        {
            
        }

        public void Save<T>(T theEvent) where T : Event
        {
            var eventSource = new EventSourcing
            {
                Action = typeof(T).Name,
                AggregateId = "",
                SagaId = theEvent.SagaId,
                Body = JsonConvert.SerializeObject(theEvent),
                TimeStamp = DateTime.Now
            };
            _eventSourcingRepository.Store(eventSource);
        }
    }

    public class EventSourcingRepository
    {
        private readonly Repository _repository;

        public EventSourcingRepository()
        {
            _repository = new Repository();
        }

        public void Store(EventSourcing eventSourcing)
        {
            _repository.EventsSource.Add(eventSourcing);
            _repository.SaveChanges();
        }
    }
}
