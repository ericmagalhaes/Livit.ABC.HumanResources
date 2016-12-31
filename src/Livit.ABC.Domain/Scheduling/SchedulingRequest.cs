using System;
using Livit.ABC.Infraestructure.Common;

namespace Livit.ABC.Domain.Scheduling
{

    public class SchedulingRequest : Aggregate
    {
        public SchedulingRequest()
        {
            Id = Guid.NewGuid();
        }
        public string RequestedBy { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Description { get; private set; }
        

        public void Apply(SchedulingRequestCreatedEvent evt)
        {
            StartDate = evt.StartDate;
            EndDate = evt.EndDate;
            RequestedBy = evt.RequestedBy;
            
        }

        public ScheduleInfo ToScheduleInfo()
        {
            var task = new TaskActivity
            {
                Id = Id.ToString(),
                Created = DateTime.Now
            };
            task.Modified = task.Created;
            task.RequestedBy = RequestedBy;

            var scheduleInfo = new ScheduleInfo
            {
                Id = Guid.NewGuid().ToString(),
                TaskActivity = task,
                StartDate = StartDate,
                EndDate = EndDate
            };
            
            return scheduleInfo;
        }

        public class Factory
        {
            public static SchedulingRequest Create(string requestedBy, DateTime startDate, DateTime endDate,string description)
            {
                var requested = new SchedulingRequestCreatedEvent(requestedBy, startDate, endDate, description);
                var root = new SchedulingRequest();
                root.RaiseEvent(requested);
                return root;
            }
        }

    }
}