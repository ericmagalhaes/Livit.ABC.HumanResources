using System;
using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.CommandStack.Events
{
    public class LeaveCreatedEvent : Event
    {
        public LeaveCreatedEvent(Guid requestId, string leaveId)
        {
            LeaveId = leaveId;
        }
        public string LeaveId { get; set; }
        public Guid RequestId { get; set; }

    }
}