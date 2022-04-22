using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleTicket.MVC.Models
{
    // .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------. 
    //| .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |
    //| |   ______     | || |     ______   | || |    _______   | || |  _________   | || |  _______     | || | ____ ____    | || |     _____    | || |     ______   | || |  _________   | |
    //| |  |_ __   \   | || |   .' ___  |  | || |   /  ___  |  | || | |_   ___  |  | || | |_   __ \    | || ||_  _| |_  _| | || |    |_   _|   | || |   .' ___  |  | || | |_ ___  |  | |
    //| |    | |__) |  | || |  / .'   \_|  | || |  |  (__ \_|  | || |   | |_  \_|  | || |   | |__) |   | || |  \ \   / /   | || |      | |     | || |  / .'   \_|  | || |   | |_  \_|  | |
    //| |    |  ___/   | || |  | |         | || |   '.___`-.   | || |   |  _|  _   | || |   |  __ /    | || |   \ \ / /    | || |      | |     | || |  | |         | || |   |  _|  _   | |
    //| |   _| |_      | || |  \ `.___.'\  | || |  |`\____) |  | || |  _| |___/ |  | || |  _| |  \ \_  | || |    \ ' /     | || |     _| |_    | || |  \ `.___.'\  | || |  _| |___/ |  | |
    //| |  |_____|     | || |   `._____.'  | || |  |_______.'  | || | |_________|  | || | |____| |___| | || |     \_/      | || |    |_____|   | || |   `._____.'  | || | |_________|  | |
    //| |              | || |              | || |              | || |              | || |              | || |              | || |              | || |              | || |              | |
    //| '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |
    // '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------' 

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
