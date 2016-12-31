using System.ComponentModel.DataAnnotations;

namespace Livit.ABC.LeaveApi.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
