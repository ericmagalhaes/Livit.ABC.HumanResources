using System;

namespace Livit.ABC.LeaveApi.Controllers
{
    public class RequestAbsence
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class RequestLeave
    {
        public DateTime LeftDate { get; set; }
    }

    public class RequestApprovment
    {
        public string HumanResourcesRequestId { get; set; }
        public bool IsApproved { get; set; }
    }
}