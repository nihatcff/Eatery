using System.ComponentModel.DataAnnotations;

namespace Eatery.ViewModels.ChefViewModels
{
    public class ChefCreateVM
    {
        [Required,MaxLength(512),MinLength(3)]
        public string Fullname { get; set; } = string.Empty;
        [Required,MaxLength(1024),MinLength(3)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public IFormFile Image { get; set; } = null!;
        [Required]
        public int CategoryId { get; set; }
    }

}
