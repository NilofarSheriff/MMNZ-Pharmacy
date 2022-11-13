using System.ComponentModel.DataAnnotations;

namespace NiloPharmacy.Data.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
