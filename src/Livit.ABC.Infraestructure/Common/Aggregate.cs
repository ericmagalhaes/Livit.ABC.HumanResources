using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Livit.ABC.Infraestructure.Broker;

namespace Livit.ABC.Infraestructure.Common
{
    public abstract class Aggregate : IAggregate
    {
        
        public Guid Id { get; protected set; }

        private readonly IList<DomainEvent> _uncommittedEvents = new List<DomainEvent>();

        Guid IAggregate.Id => Id;

        bool IAggregate.HasPendingChanges => _uncommittedEvents.Any();

        IEnumerable<DomainEvent> IAggregate.GetUncommittedEvents()
        {
            return _uncommittedEvents.ToArray();
        }

        void IAggregate.ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }
        protected void RaiseEvent(DomainEvent evt)
        {
            _uncommittedEvents.Add(evt);
            dynamic aggregate = this;
            aggregate.Apply((dynamic)evt);
            
            
        }
    }

}