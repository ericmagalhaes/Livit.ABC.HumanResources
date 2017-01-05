using System;
using System.ComponentModel.DataAnnotations;

namespace Livit.ABC.LeaveApi.Models
{
    public class RequestAbsence
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}