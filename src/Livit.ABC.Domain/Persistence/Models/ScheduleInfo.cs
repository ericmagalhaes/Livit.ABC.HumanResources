using System;

namespace Livit.ABC.Domain.Scheduling
{
    public class ScheduleInfo
    {
        public string Id { get; set; }
        public TaskActivity TaskActivity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProviderId { get; set; }

        public string ProviderScheduleId { get; set; }
        public string Description { get; set; }
    }
}