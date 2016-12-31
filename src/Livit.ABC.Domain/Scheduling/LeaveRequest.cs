using System;
using Livit.ABC.Domain.Shared;

namespace Livit.ABC.Domain.Scheduling
{
    public class LeaveRequest : SchedulingHumanResourcesRequest
    {
        
        /// <summary>
        /// Process Id
        /// </summary>
        public override string ProcessId => "HR002";
        /// <summary>
        /// Process description
        /// </summary>
        public override string ProcessDescription => $"User {RequestedBy} request a left date on {EndDate}";

        public LeaveRequest(string id, bool needsApproval, DateTime created, DateTime modified, string requestedBy, DateTime endDate, string modifiedBy = null) : base(id, needsApproval, created, modified, requestedBy, DateTime.MinValue, endDate, modifiedBy)
        {
        }
        public class Factory
        {
            public static LeaveRequest Create(string id, string requestBy,DateTime endDate)
            {
                var created = DateTime.Now;
                var modified = created;
                var leaveRequest = new LeaveRequest(
                    id,
                    true,
                    created,
                    modified, requestBy, endDate);
                return leaveRequest;
            }
        }
    }
}