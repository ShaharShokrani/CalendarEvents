using System.ComponentModel.DataAnnotations;

namespace CalendarEvents.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}