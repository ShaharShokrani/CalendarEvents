using System.ComponentModel.DataAnnotations;

namespace CalendarEvents.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
