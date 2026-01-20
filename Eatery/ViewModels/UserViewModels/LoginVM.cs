using System.ComponentModel.DataAnnotations;

namespace Eatery.ViewModels.UserViewModels
{
    public class LoginVM
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(512), MinLength(3), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
