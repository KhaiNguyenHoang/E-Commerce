using E_Commerce.Models;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class ProductController(
        IProductService productService,
        ICategoryService categoryService,
        IReviewService reviewService) : BaseController
    {
        private readonly IProductService _productService = productService;
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IReviewService _reviewService = reviewService;

        // GET: /Product
        public async Task<IActionResult> Index(int? categoryId, string? search, string? sortBy, int page = 1, int pageSize = 12)
        {
            IEnumerable<Product> products;

            if (!string.IsNullOrEmpty(search))
            {
                products = await _productService.SearchAsync(search);
                ViewBag.Search = search;
            }
            else if (categoryId.HasValue)
            {
                products = await _productService.GetByCategoryAsync(categoryId.Value);
                ViewBag.CategoryId = categoryId;
            }
            else
            {
                products = await _productService.GetActiveAsync();
            }

            // Sorting
            products = sortBy switch
            {
                "price_asc" => products.OrderBy(p => p.Price),
                "price_desc" => products.OrderByDescending(p => p.Price),
                "name" => products.OrderBy(p => p.Name),
                "newest" => products.OrderByDescending(p => p.CreatedAt),
                _ => products
            };
            ViewBag.SortBy = sortBy;

            // Pagination
            var totalItems = products.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var pagedProducts = products.Skip((page - 1) * pageSize).Take(pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.Categories = await _categoryService.GetActiveAsync();
            
            return View(pagedProducts);
        }

        // GET: /Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            ViewBag.Variants = await _productService.GetVariantsByProductIdAsync(id);
            ViewBag.Images = await _productService.GetImagesByProductIdAsync(id);
            
            // Review data
            ViewBag.Reviews = await _reviewService.GetApprovedByProductIdAsync(id);
            ViewBag.CanReview = false;
            
            var userId = await GetCurrentUserIdAsync();
            if (userId != null)
            {
                ViewBag.CanReview = await _reviewService.CanReviewAsync(userId.Value, id);
            }
            
            return View(product);
        }

        // GET: /Product/Create (Staff+)
        [RequireRole("Staff", "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetActiveAsync();
            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireRole("Staff", "Admin")]
        public async Task<IActionResult> Create(Product product, IFormFile? imageFile, string? imageUrl)
        {
            try
            {
                // Handle image - file upload or URL
                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileService = HttpContext.RequestServices.GetRequiredService<IFileService>();
                    product.MainImageUrl = await fileService.UploadImageAsync(imageFile);
                }
                else if (!string.IsNullOrEmpty(imageUrl))
                {
                    product.MainImageUrl = imageUrl;
                }

                await _productService.CreateAsync(product);
                TempData["Success"] = "Product created successfully";
                return RedirectToAction(nameof(Manage));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Categories = await _categoryService.GetActiveAsync();
                return View(product);
            }
        }

        // GET: /Product/Edit/5 (Staff+)
        [RequireRole("Staff", "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = await _categoryService.GetActiveAsync();
            ViewBag.Variants = await _productService.GetVariantsByProductIdAsync(id);
            ViewBag.Images = await _productService.GetImagesByProductIdAsync(id);
            return View(product);
        }

        // POST: /Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireRole("Staff", "Admin")]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            product.Id = id;

            try
            {
                await _productService.UpdateAsync(product);
                TempData["Success"] = "Product updated successfully";
                return RedirectToAction(nameof(Manage));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Categories = await _categoryService.GetActiveAsync();
                ViewBag.Variants = await _productService.GetVariantsByProductIdAsync(id);
                ViewBag.Images = await _productService.GetImagesByProductIdAsync(id);
                return View(product);
            }
        }

        // POST: /Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireRole("Staff", "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                TempData["Success"] = "Product deleted";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Manage));
        }

        // GET: /Product/Manage (Staff+)
        [RequireRole("Staff", "Admin")]
        public async Task<IActionResult> Manage()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        // POST: /Product/AddVariant/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireRole("Staff", "Admin")]
        public async Task<IActionResult> AddVariant(int id, ProductVariant variant)
        {
            try
            {
                await _productService.AddVariantAsync(id, variant);
                TempData["Success"] = "Variant added";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Edit), new { id });
        }

        // POST: /Product/DeleteVariant/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireRole("Staff", "Admin")]
        public async Task<IActionResult> DeleteVariant(int id, int productId)
        {
            try
            {
                await _productService.DeleteVariantAsync(id);
                TempData["Success"] = "Variant deleted";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Edit), new { id = productId });
        }

        // POST: /Product/AddImage/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireRole("Staff", "Admin")]
        public async Task<IActionResult> AddImage(int id, ProductImage image)
        {
            try
            {
                await _productService.AddImageAsync(id, image);
                TempData["Success"] = "Image added";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Edit), new { id });
        }

        // POST: /Product/DeleteImage/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireRole("Staff", "Admin")]
        public async Task<IActionResult> DeleteImage(int id, int productId)
        {
            try
            {
                await _productService.DeleteImageAsync(id);
                TempData["Success"] = "Image deleted";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Edit), new { id = productId });
        }
    }
}
