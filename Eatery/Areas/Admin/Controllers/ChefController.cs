using Eatery.Contexts;
using Eatery.Helpers;
using Eatery.Models;
using Eatery.ViewModels.ChefViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Eatery.Areas.Admin.Controllers;
[Area("Admin")]
//[Authorize(Roles ="Admin")]
public class ChefController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly string _folderPath;

    public ChefController(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
        _folderPath = Path.Combine(_environment.WebRootPath, "assets", "images");
    }

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

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await _sendCategoriesWithViewBag();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ChefCreateVM vm)
    {
        await _sendCategoriesWithViewBag();
        if (!ModelState.IsValid)
            return View(vm);

        var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);
        if (!isExistCategory)
        {
            ModelState.AddModelError("CategoryId", "This category doesn't exist");
            return View(vm);
        }

        if (!vm.Image.CheckSize(2))
        {
            ModelState.AddModelError("Image", "Size must be less than 2 mb");
            return View(vm);
        }

        if (!vm.Image.CheckType("image"))
        {
            ModelState.AddModelError("Image", "Must be image");
            return View(vm);
        }

        string uniqueFileName = await vm.Image.UploadFileAsync(_folderPath);

        Chef chef = new()
        {
            Fullname = vm.Fullname,
            Description = vm.Description,
            CategoryId = vm.CategoryId,
            ImagePath = uniqueFileName
        };

        await _context.Chefs.AddAsync(chef);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var chef = await _context.Chefs.FindAsync(id);
        if (chef is null)
            return NotFound();

        _context.Chefs.Remove(chef);
        await _context.SaveChangesAsync();

        string deletedImage = Path.Combine(_folderPath, chef.ImagePath);

        ExtensionMethods.DeleteFile(deletedImage);

        return RedirectToAction(nameof(Index));        
    }


    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        await _sendCategoriesWithViewBag();

        var chef = await _context.Chefs.FindAsync(id);
        if (chef is null)
            return NotFound();

        ChefUpdateVM vm = new()
        {
            Id = chef.Id,
            Fullname = chef.Fullname,
            Description = chef.Description,
            CategoryId = chef.CategoryId
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(ChefUpdateVM vm)
    {
        await _sendCategoriesWithViewBag();
        if (!ModelState.IsValid)
            return View(vm);

        var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);
        if (!isExistCategory)
        {
            ModelState.AddModelError("CategoryId", "This category doesn't exist");
            return View(vm);
        }

        if (!vm.Image?.CheckSize(2) ?? false)
        {
            ModelState.AddModelError("Image", "Size must be less than 2 mb");
            return View(vm);
        }

        if (!vm.Image?.CheckType("image") ?? false)
        {
            ModelState.AddModelError("Image", "Must be image");
            return View(vm);
        }

        var existUser = await _context.Chefs.FindAsync(vm.Id);
        if (existUser is null)
            return BadRequest();

        existUser.Fullname = vm.Fullname;
        existUser.Description = vm.Description;
        existUser.CategoryId = vm.CategoryId;

        if(vm.Image is { })
        {
            string newImagePath = await vm.Image.UploadFileAsync(_folderPath);
            string oldImagePath = Path.Combine(_folderPath, existUser.ImagePath);

            ExtensionMethods.DeleteFile(oldImagePath);
            existUser.ImagePath = newImagePath;
        }

        _context.Chefs.Update(existUser);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }


    private async Task _sendCategoriesWithViewBag()
    {
        var categories = await _context.Categories.ToListAsync();
        ViewBag.Categories = categories;
    }
}
