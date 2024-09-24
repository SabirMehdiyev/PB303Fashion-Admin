using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB303Fashion.DataAccessLayer;
using PB303Fashion.DataAccessLayer.Entities;
using PB303Fashion.Extensions;
using PB303Fashion.Models;

namespace PB303Fashion.Areas.AdminPanel.Controllers;

public class SliderController : AdminController
{
    private readonly AppDbContext _dbContext;

    public SliderController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index()
    {
        var sliders = await _dbContext.Sliders.ToListAsync();
        return View(sliders);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Slider slider)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        if (!slider.ImageFile.IsImage())
        {
            ModelState.AddModelError("ImageFile", "Sekil secmelisiz");

            return View();
        }

        if (!slider.ImageFile.IsAllowedSize(1))
        {
            ModelState.AddModelError("ImageFile", "Sekil olcusu max 1mb olmalidir");

            return View();
        }

        var imageName = await slider.ImageFile.GenerateFileAsync(Constants.SliderImagePath);

        slider.ImgUrl = imageName;


        await _dbContext.Sliders.AddAsync(slider);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
