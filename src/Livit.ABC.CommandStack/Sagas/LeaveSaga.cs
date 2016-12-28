using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livit.ABC.Infraestructure.Broker;
using Livit.ABC.Infraestructure.Framework.CQRS;
using Livit.ABC.Infraestructure.Framework.EventStore;

namespace Livit.ABC.CommandStack.Sagas
{
    public class LeaveRequestSaga : Saga,
        IStartWithMessage<RequestLeaveCommand>,
        IHandleMessage<RescheduleAbsenceCommand>
    {
        public LeaveRequestSaga(IBus bus, IEventStore eventStore) : base(bus, eventStore)
        {
        }

        public void Handle(RequestLeaveCommand message)
        {
            throw new NotImplementedException();
        }

        public void Handle(RescheduleAbsenceCommand message)
        {
            throw new NotImplementedException();
        }
    }
}
