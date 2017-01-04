namespace Livit.ABC.CommandStack.Commands
{
    public class RequestApprovmentCommand : IdentityCommand
    {
        public RequestApprovmentCommand(string requestedBy, string humanResourcesRequestId, bool isApproved, string managerId) : base(requestedBy)
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