using System.ComponentModel.DataAnnotations;

namespace DevFest22Asyut.ViewModels
{
    public class CreateArticleViewModel
    {
        [Required(ErrorMessage ="Id is required")]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(300)]
        public string? Title { get; set; }

        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

    }
}
