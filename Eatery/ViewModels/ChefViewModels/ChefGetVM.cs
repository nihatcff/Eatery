using Eatery.Models;

namespace Eatery.ViewModels.ChefViewModels
{
    public class ChefGetVM
    {
        public int Id { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }

}
