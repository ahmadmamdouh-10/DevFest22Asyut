using System.ComponentModel.DataAnnotations;

namespace DevFest22Asyut.ViewModels
{
    public class ContactInfoViewModel
    {
        [Required(ErrorMessage = "Id is required")]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(300)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Facebook is required")]
        [MaxLength(300)]
        public string? Facebook { get; set; }

        [Required(ErrorMessage = "Twitter is required")]
        [MaxLength(300)]
        public string? Twitter { get; set; }

        [Required(ErrorMessage = "LinkedIn is required")]
        [MaxLength(300)]
        public string? LinkedIn { get; set; }

        [Required(ErrorMessage = "Youtube is required")]
        [MaxLength(300)]
        public string? Youtube { get; set; }

    }
}
