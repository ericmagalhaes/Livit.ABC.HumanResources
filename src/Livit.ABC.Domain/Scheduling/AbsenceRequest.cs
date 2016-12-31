using System;
using Livit.ABC.Domain.Shared;

namespace Livit.ABC.Domain.Scheduling
{
    /// <summary>
    /// Absence Request
    /// </summary>
    public class AbsenceRequest : SchedulingHumanResourcesRequest
    {
        

        /// <summary>
        /// Process Id
        /// </summary>
        public override string ProcessId => "HR001";
        /// <summary>
        /// Process description
        /// </summary>
        public override string ProcessDescription => $"User {RequestedBy} request an absence period from {StartDate} to {EndDate}";

        public AbsenceRequest(
            string id, 
            bool needsApproval, 
            DateTime created, 
            DateTime modified, 
            string requestedBy, 
            DateTime startDate, 
            DateTime endDate, 
            string modifiedBy = null) : base(id, needsApproval, created, modified, requestedBy, startDate, endDate, modifiedBy)
        {

        }

        public class Factory
        {
            public static AbsenceRequest Create(string id,string requestBy,DateTime startDate,DateTime endDate)
            {
                var created = DateTime.Now;
                var modified = created;
                var absenceRequest = new AbsenceRequest(
                    id,
                    true,
                    created, 
                    modified, requestBy, startDate, endDate);
                return absenceRequest;
            }

            
        }

    }
}