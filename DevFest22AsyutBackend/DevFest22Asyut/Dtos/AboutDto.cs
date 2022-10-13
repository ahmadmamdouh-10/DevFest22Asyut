using System.ComponentModel.DataAnnotations;

namespace DevFest22Asyut.Dtos
{
    public class AboutDto
    {
        [Required]
        public string? Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }
    }
}
