using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.CommandStack.Events
{
    public class SetApprovalStatusRejectedEvent : Event
    {
        public SetApprovalStatusRejectedEvent(string requestId, string description)
        {
            RequestId = requestId;
            Description = description;
        }

        public string RequestId { get; private set; }
        public string Description { get; private set; }
    }
}