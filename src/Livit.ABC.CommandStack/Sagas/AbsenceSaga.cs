using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livit.ABC.Domain;
using Livit.ABC.Infraestructure.Broker;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Framework.EventStore;

namespace Livit.ABC.CommandStack.Sagas
{
    /// <summary>
    /// Saga works like a process manager
    /// It`ll route the command to their specific handle
    /// </summary>
    public class AbsenceRequestSaga : Saga,
        IStartWithMessage<RequestAbsenceCommand>,
        IHandleMessage<RescheduleAbsenceCommand>
    {
        public AbsenceRequestSaga(IBus bus, IEventStore eventStore) : base(bus, eventStore)
        {
        }

        public void Handle(RequestAbsenceCommand message)
        {
            var request = AbsenceRequestRoot.Create(message.RequestedBy, message.StartDate, message.EndDate);
            var response = 
        }

        public void Handle(RescheduleAbsenceCommand message)
        {
            throw new NotImplementedException();
        }
    }
    
}
