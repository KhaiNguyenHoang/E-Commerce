using E_Commerce.Models;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace E_Commerce.Controllers
{
    public class BaseController : Controller
    {
        protected IAuthService? AuthService => HttpContext.RequestServices.GetService<IAuthService>();

        protected async Task<User?> GetCurrentUserAsync()
        {
            return await AuthService?.GetCurrentUserAsync()!;
        }

        protected async Task<int?> GetCurrentUserIdAsync()
        {
            var user = await GetCurrentUserAsync();
            return user?.Id;
        }

        protected async Task<bool> IsAuthenticatedAsync()
        {
            return await GetCurrentUserAsync() != null;
        }

        protected async Task<bool> IsInRoleAsync(string roleName)
        {
            var user = await GetCurrentUserAsync();
            return user?.Role?.Name == roleName;
        }

        protected async Task<bool> IsAdminAsync() => await IsInRoleAsync("Admin");
        protected async Task<bool> IsStaffAsync() => await IsInRoleAsync("Staff");
        protected async Task<bool> IsCustomerAsync() => await IsInRoleAsync("Customer");
        protected async Task<bool> IsStaffOrAdminAsync() => await IsAdminAsync() || await IsStaffAsync();
    }

    // Filter for requiring authentication
    public class RequireAuthAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authService = context.HttpContext.RequestServices.GetService<IAuthService>();
            var user = await authService?.GetCurrentUserAsync()!;

            if (user == null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            await next();
        }
    }

    // Filter for requiring specific role
    public class RequireRoleAttribute : ActionFilterAttribute
    {
        private readonly string[] _roles;

        public RequireRoleAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authService = context.HttpContext.RequestServices.GetService<IAuthService>();
            var user = await authService?.GetCurrentUserAsync()!;

            if (user == null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            if (!_roles.Contains(user.Role?.Name))
            {
                // Set error message and redirect to home
                var controller = context.Controller as Controller;
                controller?.TempData?.Add("Error", "Access Denied. You don't have permission to access this page.");
                context.Result = new RedirectToActionResult("Index", "Home", null);
                return;
            }

            await next();
        }
    }
}
