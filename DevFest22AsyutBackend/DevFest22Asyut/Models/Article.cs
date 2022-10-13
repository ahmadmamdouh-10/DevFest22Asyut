using System.ComponentModel.DataAnnotations;

namespace DevFest22Asyut.Models
{
    public class Article : BaseModel
    {
        public Article()
        {

        }

        public Article(string title, string image, string description)
        {
            Title = title;
            Image = image;
            Description = description;
        }

        [Required]
        [MaxLength(300)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Image { get; set; }

        [Required]
        public string? Description { get; set; }
    }
}
