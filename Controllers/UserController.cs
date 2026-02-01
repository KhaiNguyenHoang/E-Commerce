using E_Commerce.Models;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [RequireRole("Admin")]
    public class UserController(IUserService userService) : BaseController
    {
        private readonly IUserService _userService = userService;

        // GET: /User
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllAsync();
            return View(users);
        }

        // POST: /User/ToggleActive/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null) return NotFound();

                await _userService.SetActiveStatusAsync(id, !user.IsActive);
                TempData["Success"] = $"User {(!user.IsActive ? "activated" : "deactivated")}";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
