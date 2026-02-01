using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers;

public class HomeController : BaseController
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public HomeController(
        IProductService productService,
        ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllAsync();
        var featuredProducts = (await _productService.GetAllAsync())
            .Where(p => p.IsFeatured && p.IsActive)
            .Take(8)
            .ToList();
        var latestProducts = (await _productService.GetAllAsync())
            .Where(p => p.IsActive)
            .OrderByDescending(p => p.CreatedAt)
            .Take(8)
            .ToList();

        ViewBag.Categories = categories;
        ViewBag.FeaturedProducts = featuredProducts;
        ViewBag.LatestProducts = latestProducts;
        
        return View();
    }

    public IActionResult About() => View();
    
    public IActionResult Contact() => View();
    
    public IActionResult Error() => View();
}
