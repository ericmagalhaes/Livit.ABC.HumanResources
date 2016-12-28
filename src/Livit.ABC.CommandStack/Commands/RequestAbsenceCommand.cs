﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Livit.ABC.CommandStack
{
    public class RequestAbsenceCommand : IdentityCommand
    {
        
        /// <summary>
        /// begining of absence
        /// </summary>
        public DateTime StartDate { get; private set; }
        /// <summary>
        /// end of absence
        /// </summary>
        public DateTime EndDate { get; private set; }
        
        /// <summary>
        /// Initiate a new absence command
        /// </summary>
        /// <param name="requestedBy">request user id </param>
        /// <param name="startDate">start date of the absence</param>
        /// <param name="endDate">end date of the absence</param>
        public RequestAbsenceCommand(string requestedBy, DateTime startDate, DateTime endDate) : base(requestedBy)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}