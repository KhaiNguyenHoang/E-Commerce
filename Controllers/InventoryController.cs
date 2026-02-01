using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers;

[RequireRole("Admin", "Staff")]
public class InventoryController : BaseController
{
    private readonly IInventoryService _inventoryService;
    private readonly IProductService _productService;

    public InventoryController(IInventoryService inventoryService, IProductService productService)
    {
        _inventoryService = inventoryService;
        _productService = productService;
    }

    // GET: /Inventory
    public async Task<IActionResult> Index(int threshold = 10)
    {
        var lowStock = await _inventoryService.GetLowStockItemsAsync(threshold);
        ViewBag.Threshold = threshold;
        return View(lowStock);
    }

    // POST: /Inventory/UpdateStock
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStock(int variantId, int quantity)
    {
        await _inventoryService.UpdateStockAsync(variantId, quantity);
        TempData["Success"] = "Stock updated successfully";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Inventory/ProductStock/5
    public async Task<IActionResult> ProductStock(int id)
    {
        var variants = await _productService.GetVariantsByProductIdAsync(id);
        var product = await _productService.GetByIdAsync(id);
        ViewBag.Product = product;
        return View(variants);
    }
}
