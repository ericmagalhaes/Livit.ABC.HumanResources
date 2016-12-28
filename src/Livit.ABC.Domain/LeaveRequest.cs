using System;

namespace Livit.ABC.Domain
{
    public class LeaveRequest : HumanResourcesRequest
    {
        /// <summary>
        /// employee leave date
        /// </summary>
        public DateTime EndDate { get; private set; }
        
        /// <summary>
        /// Human resources task unique identitfier
        /// </summary>
        public override string HumanResourcesTaskId => "HR002";
        public LeaveRequest(string id, bool needsApproval, DateTime created, DateTime modified, Employee createdBy, string humanResourcesRequestActivity, DateTime endDate, Employee modifiedBy = null) : base(id, needsApproval, created, modified, createdBy, humanResourcesRequestActivity, modifiedBy)
        {
            EndDate = endDate;
        }
    }
}