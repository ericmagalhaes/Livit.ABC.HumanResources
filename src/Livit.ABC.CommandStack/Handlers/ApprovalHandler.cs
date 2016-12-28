using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livit.ABC.CommandStack.Events;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Framework.EventStore;

namespace Livit.ABC.CommandStack.Handlers
{
    public class ApprovalHandler : Handler,
        IHandleMessage<AbsenceCreatedEvent>
    {
        public ApprovalHandler(IEventStore eventStore) : base(eventStore)
        {
        }

        public void Handle(AbsenceCreatedEvent message)
        {
            throw new NotImplementedException();
        }
    }
}
