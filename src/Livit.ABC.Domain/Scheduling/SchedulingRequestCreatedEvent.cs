using System;
using Livit.ABC.Infraestructure.Common;

namespace Livit.ABC.Domain.Scheduling
{
    public class SchedulingRequestCreatedEvent : DomainEvent
    {
        public string RequestedBy { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Description { get; private set; }
        public SchedulingRequestCreatedEvent(string requestedBy, DateTime startDate, DateTime endDate,string description)
        {
            RequestedBy = requestedBy;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }
    }
}