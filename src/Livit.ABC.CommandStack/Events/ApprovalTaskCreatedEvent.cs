using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.CommandStack.Events
{
    /// <summary>
    /// created when a manager approves a request
    /// </summary>
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
