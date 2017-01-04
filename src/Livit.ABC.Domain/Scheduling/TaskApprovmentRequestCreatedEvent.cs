using Livit.ABC.Infraestructure.Common;

namespace Livit.ABC.Domain.Scheduling
{
    public class TaskApprovmentRequestCreatedEvent : DomainEvent
    {
        public TaskApprovmentRequestCreatedEvent(string humanResourcesRequestId, bool isApproved, string managerId)
        {
            HumanResourcesRequestId = humanResourcesRequestId;
            IsApproved = isApproved;
            ManagerId = managerId;
        }

        public string HumanResourcesRequestId { get; private set; }
        public bool IsApproved { get; private set; }
        public string ManagerId { get; private set; }
    }
}