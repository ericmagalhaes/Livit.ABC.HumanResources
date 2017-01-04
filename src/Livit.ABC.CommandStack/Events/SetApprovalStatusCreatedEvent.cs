using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.CommandStack.Events
{
    public class SetApprovalStatusCreatedEvent : Event
    {
        public SetApprovalStatusCreatedEvent(string requestId)
        {
            RequestId = requestId;
        }
        public string RequestId { get; private set; }
    }
}