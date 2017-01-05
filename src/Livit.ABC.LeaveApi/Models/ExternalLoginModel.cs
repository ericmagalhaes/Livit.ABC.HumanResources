using System.ComponentModel.DataAnnotations;

namespace Livit.ABC.LeaveApi.Models
{
    public class ExternalLoginModel
    {
        [Required]
        public string Provider { get; set; }
        [Required]
        public string ReturnUrl { get; set; }
    }
}