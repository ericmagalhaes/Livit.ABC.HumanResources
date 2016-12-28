using System;
using Livit.ABC.Infraestructure.Common;

namespace Livit.ABC.Domain
{
    public class AbsenceRequestRoot : Aggregate
    {
        public AbsenceRequestRoot()
        {
            Id = Guid.NewGuid();
        }
        public string UserId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public void Apply(AbsenceRequestCreatedEvent evt)
        {
            StartDate = evt.StartDate;
            EndDate = evt.EndDate;
            UserId = evt.UserId;

        }

        public static AbsenceRequestRoot Create(string userId,DateTime startDate, DateTime endDate)
        {
            var requested = new AbsenceRequestCreatedEvent(userId, startDate, endDate);
            var root = new AbsenceRequestRoot();
            root.RaiseEvent(requested);
            return root;
        }

    }
}