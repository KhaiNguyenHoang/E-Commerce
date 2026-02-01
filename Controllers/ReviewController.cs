using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class ReviewController(IReviewService reviewService) : BaseController
    {
        private readonly IReviewService _reviewService = reviewService;

        // GET: /Review/Product/5 (Public)
        public async Task<IActionResult> Product(int id)
        {
            var reviews = await _reviewService.GetApprovedByProductIdAsync(id);
            ViewBag.ProductId = id;
            ViewBag.AverageRating = await _reviewService.GetAverageRatingAsync(id);
            return View(reviews);
        }

        // GET: /Review/MyReviews (Customer)
        [RequireAuth]
        public async Task<IActionResult> MyReviews()
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            var reviews = await _reviewService.GetByUserIdAsync(userId.Value);
            return View(reviews);
        }

        // GET: /Review/Create/5 (Customer)
        [RequireAuth]
        public async Task<IActionResult> Create(int productId)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            var canReview = await _reviewService.CanReviewAsync(userId.Value, productId);
            if (!canReview)
            {
                TempData["Error"] = "You can only review products you have purchased";
                return RedirectToAction("Details", "Product", new { id = productId });
            }

            ViewBag.ProductId = productId;
            return View();
        }

        // POST: /Review/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireAuth]
        public async Task<IActionResult> Create(int productId, int rating, string? comment)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            try
            {
                await _reviewService.AddReviewAsync(userId.Value, productId, rating, comment);
                TempData["Success"] = "Review submitted. It will appear after approval.";
                return RedirectToAction("Details", "Product", new { id = productId });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.ProductId = productId;
                return View();
            }
        }

        // GET: /Review/Edit/5 (Customer)
        [RequireAuth]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            var reviews = await _reviewService.GetByUserIdAsync(userId.Value);
            var review = reviews.FirstOrDefault(r => r.Id == id);

            if (review == null) return NotFound();

            return View(review);
        }

        // POST: /Review/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireAuth]
        public async Task<IActionResult> Edit(int id, int rating, string? comment)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            try
            {
                await _reviewService.UpdateReviewAsync(userId.Value, id, rating, comment);
                TempData["Success"] = "Review updated. It will appear after re-approval.";
                return RedirectToAction(nameof(MyReviews));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // POST: /Review/Delete/5 (Customer)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireAuth]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            try
            {
                await _reviewService.DeleteReviewAsync(userId.Value, id);
                TempData["Success"] = "Review deleted";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(MyReviews));
        }

        // GET: /Review/Pending (Staff+)
        [RequireRole("Staff", "Admin")]
        public async Task<IActionResult> Pending()
        {
            var reviews = await _reviewService.GetPendingReviewsAsync();
            return View(reviews);
        }

        // POST: /Review/Approve/5 (Staff+)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireRole("Staff", "Admin")]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                await _reviewService.ApproveReviewAsync(id);
                TempData["Success"] = "Review approved";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Pending));
        }

        // POST: /Review/Reject/5 (Staff+)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireRole("Staff", "Admin")]
        public async Task<IActionResult> Reject(int id)
        {
            try
            {
                await _reviewService.RejectReviewAsync(id);
                TempData["Success"] = "Review rejected";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Pending));
        }
    }
}
