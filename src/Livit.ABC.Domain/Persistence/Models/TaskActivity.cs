using System;

namespace Livit.ABC.Domain.Scheduling
{
    public class TaskActivity
    {
        public string Id { get; set; }
        public string RequestedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool NeedsApproval { get; set; }

    }
}