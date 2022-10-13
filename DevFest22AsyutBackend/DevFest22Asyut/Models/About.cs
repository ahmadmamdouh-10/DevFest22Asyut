using System.ComponentModel.DataAnnotations;

namespace DevFest22Asyut.Models
{
    public class About : BaseModel
    {
        [Required]
        [MaxLength(300)]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }
    }
}
