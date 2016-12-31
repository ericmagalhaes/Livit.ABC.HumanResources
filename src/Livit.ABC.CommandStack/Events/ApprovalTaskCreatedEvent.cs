using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.CommandStack.Events
{
    public class ApprovalTaskCreatedEvent : Event
    {
        public ApprovalTaskCreatedEvent(string requestId, string approvalTaskId)
        {
            RequestId = requestId;
            ApprovalTaskId = approvalTaskId;
        }
        public string ApprovalTaskId { get; private set; }
        public string RequestId { get; private set; }
    }

}
