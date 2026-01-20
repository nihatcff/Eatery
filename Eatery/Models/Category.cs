using Eatery.Models.Common;

namespace Eatery.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Chef> Chefs { get; set; } = [];
    }
}
