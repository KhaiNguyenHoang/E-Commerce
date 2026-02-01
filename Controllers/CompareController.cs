using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers;

public class CompareController : BaseController
{
    private readonly IComparisonService _comparisonService;
    private readonly IProductService _productService;

    public CompareController(IComparisonService comparisonService, IProductService productService)
    {
        _comparisonService = comparisonService;
        _productService = productService;
    }

    // GET: /Compare
    public async Task<IActionResult> Index()
    {
        var ids = _comparisonService.GetProductIds();
        var products = new List<E_Commerce.Models.Product>();
        
        foreach (var id in ids)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product != null) products.Add(product);
        }
        
        return View(products);
    }

    // POST: /Compare/Add/5
    [HttpPost]
    public IActionResult Add(int id)
    {
        if (!_comparisonService.CanAdd)
        {
            TempData["Error"] = "You can only compare up to 4 products";
            return RedirectToAction("Details", "Product", new { id });
        }

        _comparisonService.AddProduct(id);
        TempData["Success"] = "Product added to comparison";
        return RedirectToAction("Details", "Product", new { id });
    }

    // POST: /Compare/Remove/5
    [HttpPost]
    public IActionResult Remove(int id)
    {
        _comparisonService.RemoveProduct(id);
        return RedirectToAction(nameof(Index));
    }

    // POST: /Compare/Clear
    [HttpPost]
    public IActionResult Clear()
    {
        _comparisonService.Clear();
        return RedirectToAction(nameof(Index));
    }
}
