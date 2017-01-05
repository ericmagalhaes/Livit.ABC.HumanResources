using System;
using System.ComponentModel.DataAnnotations;

namespace Livit.ABC.LeaveApi.Models
{
    public class RequestLeave
    {
        [Required]
        public DateTime LeftDate { get; set; }
    }
}