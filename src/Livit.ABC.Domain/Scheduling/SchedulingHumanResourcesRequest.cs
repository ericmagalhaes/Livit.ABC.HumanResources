using System;
using Livit.ABC.Domain.Shared;

namespace Livit.ABC.Domain.Scheduling
{
    public abstract class SchedulingHumanResourcesRequest : HumanResourcesRequest
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public SchedulingHumanResourcesRequest(string id, bool needsApproval, DateTime created, DateTime modified, string requestedBy, DateTime startDate, DateTime endDate, string modifiedBy = null) : base(id, needsApproval, created, modified, requestedBy, modifiedBy)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}