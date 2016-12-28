using System;

namespace Livit.ABC.CommandStack
{
    /// <summary>
    /// reschedule an existing absence
    /// </summary>
    public class RescheduleLeaveCommand : IdentityCommand
    {
        /// <summary>
        /// absence id
        /// </summary>
        public string LeaveId { get; private set; }
        
        /// <summary>
        /// updated left date
        /// </summary>
        public DateTime LeftDate { get; private set; }

        /// <summary>
        /// reschedule an existing absence
        /// </summary>
        /// <param name="requestedBy">request user id</param>
        /// <param name="leaveId">absence id</param>
        /// <param name="leftDate">updated end date</param>
        public RescheduleLeaveCommand(string requestedBy, string leaveId, DateTime leftDate) : base(requestedBy)
        {
            LeaveId = leaveId;
            
            LeftDate = leftDate;
        }
    }
}