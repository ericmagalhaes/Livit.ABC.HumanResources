using System;

namespace Livit.ABC.Domain.Shared
{
    /// <summary>
    /// Human Resources Request
    /// Base class for all HR requests
    /// LSP - Liskov substitution principle
    /// </summary>
    public abstract class HumanResourcesRequest : Request
    {
        public virtual string ProcessId
        {
            get { throw new NotImplementedException(); }
        }

        public virtual string ProcessDescription
        {
            get { throw new NotImplementedException(); }
        }

        protected HumanResourcesRequest(string id, bool needsApproval, DateTime created, DateTime modified, string requestedBy, string modifiedBy = null) : base(id, needsApproval, created, modified, requestedBy, modifiedBy)
        {
        
        }
    }
}