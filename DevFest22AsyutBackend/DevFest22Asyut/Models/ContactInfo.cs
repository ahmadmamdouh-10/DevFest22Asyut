using System.ComponentModel.DataAnnotations;

namespace DevFest22Asyut.Models
{
    public class ContactInfo : BaseModel
    {
        [Required]
        [MaxLength(300)]
        public string? Email { get; set; }  
        [Required]
        [MaxLength(300)]
        public string? Facebook { get; set; }

        [Required]
        [MaxLength(300)]
        public string? Twitter { get; set; }

        [Required]
        [MaxLength(300)]
        public string? LinkedIn { get; set; }

        [Required]
        [MaxLength(300)]
        public string? Youtube { get; set; }
    }
}
