using System;

namespace Livit.ABC.Domain.Scheduling
{
    public class EventSourcing
    {
        public string Id { get; set; }
        public string Action { get; set; }
        public string AggregateId { get; set; }
        public string Body { get; set; }
        public string SagaId { get; set; }
        public DateTime TimeStamp { get; set; }
        
    }
}