using System.ComponentModel.DataAnnotations;

namespace DevFest22Asyut.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(4)]
        public string? Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }


        public string? ReturnUrl { get; set; }
    }
}
