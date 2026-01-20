using System.ComponentModel.DataAnnotations;

namespace Eatery.ViewModels.UserViewModels
{
    public class RegisterVM
    {
        [Required,MaxLength(512),MinLength(3)]
        public string Fullname { get; set; } = string.Empty;
        [Required,EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required,MaxLength(512),MinLength(3)]
        public string UserName { get; set; } = string.Empty;
        [Required, MaxLength(512),MinLength(3),DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required, MaxLength(512),MinLength(3),DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
