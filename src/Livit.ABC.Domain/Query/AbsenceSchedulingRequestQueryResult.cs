using System;
using Livit.ABC.Infraestructure.Framework.CQRS;

namespace Livit.ABC.Domain.Query
{
    public class AbsenceSchedulingRequestQueryResult : IQueryResult
    {
        public string Id { get; set; }
        public string RequestedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsApproved { get; set; }
        public string Approver { get; set; }
    }
    public class LeaveSchedulingRequestQueryResult : IQueryResult
    {
        public string Id { get; set; }
        public string RequestedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public DateTime LeftDate { get; set; }
        public bool IsApproved { get; set; }
        public string Approver { get; set; }
    }

    public class TaskApprovmentRequestQueryResult : IQueryResult
    {
        public string Id { get; set; }
        public string RequestedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool IsApproved { get; set; }
        public string Approver { get; set; }
        public string Description { get; set; }
    }
}