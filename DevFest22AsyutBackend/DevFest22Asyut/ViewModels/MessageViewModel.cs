using System.ComponentModel.DataAnnotations;

namespace DevFest22Asyut.ViewModels
{
    public class MessageViewModel
    {
        [Required]
        public string? Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Message { get; set; }

        [Required]
        [MaxLength(15)]
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
