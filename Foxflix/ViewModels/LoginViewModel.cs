using System.ComponentModel.DataAnnotations;

namespace Foxflix.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter email")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}