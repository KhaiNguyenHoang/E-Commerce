using E_Commerce.Models;

namespace E_Commerce.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(string fullName, string email, string password, string? phoneNumber = null);
        Task<User?> LoginAsync(string email, string password);
        Task LogoutAsync();
        Task<User?> GetCurrentUserAsync();
        Task<bool> IsInRoleAsync(int userId, string roleName);
        Task<bool> IsAdminAsync();
        // Password Reset
        Task ForgotPasswordAsync(string email);
        Task ResetPasswordAsync(string email, string token, string newPassword);
    }
}
