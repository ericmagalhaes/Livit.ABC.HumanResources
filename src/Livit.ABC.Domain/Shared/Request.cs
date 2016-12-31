using System;

namespace Livit.ABC.Domain.Shared
{
    /// <summary>
    /// Base Request
    /// responsable for the default attributes of a request
    /// </summary>
    public abstract class Request
    {
        protected Request(string id, bool needsApproval, DateTime created, DateTime modified, string requestedBy, string modifiedBy = null)
        {
            Id = id;
            NeedsApproval = needsApproval;
            Created = created;
            Modified = modified;
            RequestedBy = requestedBy;
            ModifiedBy = modifiedBy;
        }
        /// <summary>
        /// request unique identifier
        /// </summary>
        public virtual string Id { get; private set; }
        /// <summary>
        /// identifies if the request needs approval
        /// </summary>
        public virtual bool NeedsApproval { get; private set; }
        /// <summary>
        /// request created date time
        /// </summary>
        public virtual DateTime Created { get; private set; }
        /// <summary>
        /// request modified date time
        /// </summary>
        public virtual DateTime Modified { get; private set; }
        public virtual string RequestedBy { get; private set; }
        public virtual string ModifiedBy { get; private set; }
    }
}