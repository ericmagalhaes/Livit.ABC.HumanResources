using System.ComponentModel.DataAnnotations;

namespace Livit.ABC.LeaveApi.Models
{
    public class RequestApprovment
    {
        [Required]
        public string HumanResourcesRequestId { get; set; }
        [Required]
        public bool IsApproved { get; set; }
    }
}