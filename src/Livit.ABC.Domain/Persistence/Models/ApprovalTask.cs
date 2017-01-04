using System;
using Livit.ABC.Domain.Scheduling;

namespace Livit.ABC.Domain.Persistence.Models
{
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