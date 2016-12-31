using System.ComponentModel.DataAnnotations;

namespace Livit.ABC.LeaveApi.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
