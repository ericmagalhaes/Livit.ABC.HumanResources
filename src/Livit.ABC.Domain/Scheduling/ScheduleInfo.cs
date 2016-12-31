using System;

namespace Livit.ABC.Domain.Scheduling
{
    public class EventSourcing
    {
        public string Id { get; set; }
        public string Action { get; set; }
        public string AggregateId { get; set; }
        public string Body { get; set; }
        public string SagaId { get; set; }
        public DateTime TimeStamp { get; set; }
        
    }

    public class ScheduleInfo
    {
        public string Id { get; set; }
        public TaskActivity TaskActivity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }

    public class TaskActivity
    {
        public string Id { get; set; }
        public string RequestedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool NeedsApproval { get; set; }

    }

    public class ApprovalTask
    {
        public string Id { get; set; }
        public TaskActivity TaskActivity { get; set; }
        public string Approver { get; set; }
        public DateTime Created { get; set; }
        public bool Approved { get; set; }

        public DateTime ApprovalDate { get; set; }
    }
}