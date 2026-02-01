using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class AuthController(IAuthService authService) : BaseController
    {
        private readonly IAuthService _authService = authService;

        // GET: /Auth/Login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, string? returnUrl = null)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Email and password are required";
                return View();
            }

            var user = await _authService.LoginAsync(email, password);

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password";
                return View();
            }

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: /Auth/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Auth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string fullName, string email, string password, string confirmPassword, string? phoneNumber)
        {
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "All fields are required";
                return View();
            }

            if (password != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match";
                return View();
            }

            try
            {
                var user = await _authService.RegisterAsync(fullName, email, password, phoneNumber);
                
                if (user != null)
                {
                    // Auto login after registration
                    await _authService.LoginAsync(email, password);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

            return View();
        }

        // GET: /Auth/Logout
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: /Auth/ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // POST: /Auth/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Error = "Email is required";
                return View();
            }

            try 
            {
                await _authService.ForgotPasswordAsync(email);
                // Always show success to prevent email enumeration
                ViewBag.Success = "If an account exists with this email, a reset link has been sent.";
            }
            catch (Exception ex)
            {
                // Log error for debugging
                Console.WriteLine($"ForgotPassword Error: {ex.Message}");
                Console.WriteLine($"Stack: {ex.StackTrace}");
                ViewBag.Error = $"Error: {ex.Message}"; // Show error in dev
            }

            return View();
        }

        // GET: /Auth/ResetPassword
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            ViewBag.Email = email;
            ViewBag.Token = token;
            return View();
        }

        // POST: /Auth/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string email, string token, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match";
                ViewBag.Email = email;
                ViewBag.Token = token;
                return View();
            }

            try
            {
                await _authService.ResetPasswordAsync(email, token, newPassword);
                TempData["Success"] = "Password reset successfully. Please login.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Email = email;
                ViewBag.Token = token;
                return View();
            }
        }
    }
}
