using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.CommandStack.Events
{
    /// <summary>
    /// created when a request is denied
    /// </summary>
    public class ApprovalTaskRejectedEvent : Event
    {
        public ApprovalTaskRejectedEvent(string requestId, string description)
        {
            RequestId = requestId;
            Description = description;
        }

        public string RequestId { get; private set; }
        public string Description { get; private set; }
    }
}