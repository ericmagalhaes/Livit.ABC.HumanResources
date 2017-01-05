using System;

namespace Livit.ABC.CommandStack.Commands
{
    public class RequestLeaveCommand : IdentityCommand
    {
        /// <summary>
        /// output property
        /// </summary>
        public string RequestId { get; internal set; }
        /// <summary>
        /// left date
        /// </summary>
        public DateTime LeftDate { get; private set; }

        /// <summary>
        /// Initiate a new leave command
        /// </summary>
        /// <param name="requestedBy">request user id </param>
        /// <param name="leftDate">end date of the absence</param>
        public RequestLeaveCommand(string requestedBy, DateTime leftDate) : base(requestedBy)
        {
            LeftDate = leftDate;
        }
    }
}