using System;

namespace Livit.ABC.Domain
{
    /// <summary>
    /// Human Resources Request
    /// Base class for all HR requests
    /// LSP - Liskov substitution principle
    /// </summary>
    public abstract class HumanResourcesRequest : Request
    {
        public virtual string HumanResourcesTaskId { get; private set; }

        protected HumanResourcesRequest(string id, bool needsApproval, DateTime created, DateTime modified, Employee createdBy, string humanResourcesRequestActivity, Employee modifiedBy = null) : base(id, needsApproval, created, modified, createdBy, modifiedBy)
        {
            HumanResourcesTaskId = humanResourcesRequestActivity;
        }
    }
}