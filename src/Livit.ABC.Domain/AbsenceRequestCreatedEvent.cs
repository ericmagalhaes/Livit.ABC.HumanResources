using System;
using Livit.ABC.Infraestructure.Common;

namespace Livit.ABC.Domain
{
    public class AbsenceRequestCreatedEvent : DomainEvent
    {
        public string UserId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public AbsenceRequestCreatedEvent(string userId, DateTime startDate, DateTime endDate)
        {
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}