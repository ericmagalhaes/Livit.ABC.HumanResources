using System;
using System.Collections.Generic;

namespace Livit.ABC.Infraestructure.Common
{
    public interface IAggregate
    {
        Guid Id { get; }
        bool HasPendingChanges { get; }
        IEnumerable<DomainEvent> GetUncommittedEvents();
        void ClearUncommittedEvents(); 
    }
}