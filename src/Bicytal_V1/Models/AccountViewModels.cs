using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bicytal.Models
{

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress(ErrorMessage = "Email field is not a valid e-mail address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress(ErrorMessage = "Email field is not a valid e-mail address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password field is required")]
        [StringLength(100,ErrorMessage = "The field Password must be a character string with a minimum length of 6 and a maximum length of 100", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "\"ConfirmPassword\" and \"Password\" does not match")]
        public string ConfirmPassword { get; set; }
    }
}
