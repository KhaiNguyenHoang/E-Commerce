using E_Commerce.Models;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [RequireAuth]
    public class AccountController(
        IUserService userService,
        IAddressService addressService) : BaseController
    {
        private readonly IUserService _userService = userService;
        private readonly IAddressService _addressService = addressService;

        // GET: /Account
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            return View(user);
        }

        // GET: /Account/Edit
        public async Task<IActionResult> Edit()
        {
            var user = await GetCurrentUserAsync();
            return View(user);
        }

        // POST: /Account/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string fullName, string? phoneNumber, string? avatarUrl, IFormFile? avatarFile)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            try
            {
                // Handle avatar - file upload or URL
                string? finalAvatarUrl = avatarUrl;
                if (avatarFile != null && avatarFile.Length > 0)
                {
                    var fileService = HttpContext.RequestServices.GetRequiredService<IFileService>();
                    finalAvatarUrl = await fileService.UploadImageAsync(avatarFile, "avatars");
                }

                await _userService.UpdateProfileAsync(userId.Value, fullName, phoneNumber, finalAvatarUrl);
                TempData["Success"] = "Profile updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var user = await GetCurrentUserAsync();
                return View(user);
            }
        }

        // GET: /Account/ChangePassword
        public IActionResult ChangePassword()
        {
            return View();
        }

        // POST: /Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match";
                return View();
            }

            try
            {
                await _userService.ChangePasswordAsync(userId.Value, currentPassword, newPassword);
                TempData["Success"] = "Password changed successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: /Account/Addresses
        public async Task<IActionResult> Addresses()
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            var addresses = await _addressService.GetByUserIdAsync(userId.Value);
            return View(addresses);
        }

        // GET: /Account/AddAddress
        public IActionResult AddAddress()
        {
            return View();
        }

        // POST: /Account/AddAddress
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAddress(Address address)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            try
            {
                await _addressService.AddAsync(userId.Value, address);
                TempData["Success"] = "Address added successfully";
                return RedirectToAction(nameof(Addresses));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(address);
            }
        }

        // GET: /Account/EditAddress/5
        public async Task<IActionResult> EditAddress(int id)
        {
            var address = await _addressService.GetByIdAsync(id);
            if (address == null) return NotFound();

            return View(address);
        }

        // POST: /Account/EditAddress/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAddress(int id, Address address)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            address.Id = id;

            try
            {
                await _addressService.UpdateAsync(userId.Value, address);
                TempData["Success"] = "Address updated successfully";
                return RedirectToAction(nameof(Addresses));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(address);
            }
        }

        // POST: /Account/DeleteAddress/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            try
            {
                await _addressService.DeleteAsync(userId.Value, id);
                TempData["Success"] = "Address deleted";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Addresses));
        }

        // POST: /Account/SetDefaultAddress/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetDefaultAddress(int id)
        {
            var userId = await GetCurrentUserIdAsync();
            if (userId == null) return RedirectToAction("Login", "Auth");

            await _addressService.SetDefaultAsync(userId.Value, id);
            TempData["Success"] = "Default address updated";

            return RedirectToAction(nameof(Addresses));
        }
    }
}
