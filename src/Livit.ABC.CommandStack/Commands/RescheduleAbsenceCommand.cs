using System;

namespace Livit.ABC.CommandStack.Commands
{
    /// <summary>
    /// reschedule an existing absence
    /// </summary>
    public class RescheduleAbsenceCommand : IdentityCommand
    {
        /// <summary>
        /// absence id
        /// </summary>
        public string AbsenceId { get; private set; }
        /// <summary>
        /// updated absence start date
        /// </summary>
        public DateTime StartDate { get; private set; }
        /// <summary>
        /// updated absence end date
        /// </summary>
        public DateTime EndDate { get; private set; }

        /// <summary>
        /// reschedule an existing absence
        /// </summary>
        /// <param name="requestedBy">request user id</param>
        /// <param name="absenceId">absence id</param>
        /// <param name="startDate">updated start date</param>
        /// <param name="endDate">updated end date</param>
        public RescheduleAbsenceCommand(string requestedBy, string absenceId, DateTime startDate, DateTime endDate) : base(requestedBy)
        {
            AbsenceId = absenceId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}