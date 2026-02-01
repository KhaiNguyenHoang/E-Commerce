using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [RequireRole("Admin")]
    public class AdminController(
        IUserService userService,
        IOrderService orderService) : BaseController
    {
        private readonly IUserService _userService = userService;
        private readonly IOrderService _orderService = orderService;

        // GET: /Admin
        public async Task<IActionResult> Index()
        {
            ViewBag.TotalOrders = await _orderService.GetTotalOrdersCountAsync();
            ViewBag.TotalRevenue = await _orderService.GetTotalRevenueAsync();
            return View();
        }

        // GET: /Admin/Users
        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetAllAsync();
            return View(users);
        }

        // GET: /Admin/UserDetails/5
        public async Task<IActionResult> UserDetails(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        // POST: /Admin/ActivateUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateUser(int id)
        {
            try
            {
                await _userService.SetActiveStatusAsync(id, true);
                TempData["Success"] = "User activated";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Users));
        }

        // POST: /Admin/DeactivateUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            try
            {
                await _userService.SetActiveStatusAsync(id, false);
                TempData["Success"] = "User deactivated";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Users));
        }
    }
}
