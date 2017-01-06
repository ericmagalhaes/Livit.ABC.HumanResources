namespace Livit.ABC.CommandStack.Commands
{
    /// <summary>
    /// request an approvment
    /// </summary>
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