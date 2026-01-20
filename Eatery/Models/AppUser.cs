using Microsoft.AspNetCore.Identity;

namespace Eatery.Models
{
    public class AppUser :IdentityUser
    {
        public string Fullname { get; set; } = string.Empty;
    }
}
