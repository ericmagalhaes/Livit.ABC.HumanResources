using System;

namespace Livit.ABC.Domain
{
    /// <summary>
    /// Absence Request
    /// </summary>
    public class AbsenceRequest : HumanResourcesRequest
    {
        /// <summary>
        /// employee start absence date
        /// </summary>
        public DateTime StartDate { get; private set; }
        /// <summary>
        /// employee end absence date
        /// </summary>
        public DateTime EndDate { get; private set; }
        
        /// <summary>
        /// Human resources task unique identitfier
        /// </summary>
        public override string HumanResourcesTaskId => "HR001";

        public AbsenceRequest(string id, bool needsApproval, DateTime created, DateTime modified, Employee createdBy, string humanResourcesRequestActivity, DateTime startDate, DateTime endDate, Employee modifiedBy = null) : base(id, needsApproval, created, modified, createdBy, humanResourcesRequestActivity, modifiedBy)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}