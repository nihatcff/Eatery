using Eatery.Models.Common;

namespace Eatery.Models
{
    public class Chef:BaseEntity
    {
        public string Fullname { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;    
    }
}
