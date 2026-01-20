using Eatery.Contexts;
using Eatery.ViewModels.ChefViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Eatery.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    { 
        public async Task<IActionResult> Index()
        {
            var chefs = await _context.Chefs.Select(x => new ChefGetVM()
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Description = x.Description,
                ImagePath = x.ImagePath,
                CategoryName = x.Category.Name
            }).ToListAsync();

            return View(chefs);
        }

    }
}
