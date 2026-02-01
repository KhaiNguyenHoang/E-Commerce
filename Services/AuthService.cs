using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services
{
    public class AuthService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        ApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor,
        IEmailService emailService) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IEmailService _emailService = emailService;

        private const string UserSessionKey = "UserId";

        public async Task<User?> RegisterAsync(string fullName, string email, string password, string? phoneNumber = null)
        {
            if (await _userRepository.ExistsByEmailAsync(email))
            {
                throw new Exception("Email already registered");
            }

            var customerRole = await _roleRepository.GetByNameAsync("Customer")
                ?? throw new Exception("Customer role not found");

            var user = new User
            {
                FullName = fullName,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                PhoneNumber = phoneNumber,
                RoleId = customerRole.Id,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            
            if (user == null || !user.IsActive)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            // Update last login
            user.LastLoginAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            // Store user ID in session
            _httpContextAccessor.HttpContext?.Session.SetInt32(UserSessionKey, user.Id);

            return user;
        }

        public Task LogoutAsync()
        {
            _httpContextAccessor.HttpContext?.Session.Remove(UserSessionKey);
            return Task.CompletedTask;
        }

        public async Task<User?> GetCurrentUserAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32(UserSessionKey);
            
            if (userId == null)
            {
                return null;
            }

            return await _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> IsInRoleAsync(int userId, string roleName)
        {
            var user = await _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.Role?.Name == roleName;
        }

        public async Task<bool> IsAdminAsync()
        {
            var user = await GetCurrentUserAsync();
            return user?.Role?.Name == "Admin";
        }
        public async Task ForgotPasswordAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return; // Don't reveal valid emails

            var token = Guid.NewGuid().ToString();
            user.ResetPasswordToken = token;
            user.ResetPasswordTokenExpiry = DateTime.UtcNow.AddHours(1);
            await _dbContext.SaveChangesAsync();

            var request = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{request?.Scheme}://{request?.Host}";
            var resetLink = $"{baseUrl}/Auth/ResetPassword?email={email}&token={token}";

            await _emailService.SendEmailAsync(
                email, 
                "Reset Your Password", 
                $"Click here to reset your password: <a href='{resetLink}'>Reset Password</a>");
        }

        public async Task ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userRepository.GetByEmailAsync(email) 
                ?? throw new Exception("Invalid request");

            if (user.ResetPasswordToken != token || user.ResetPasswordTokenExpiry < DateTime.UtcNow)
            {
                throw new Exception("Invalid or expired token");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.ResetPasswordToken = null;
            user.ResetPasswordTokenExpiry = null;
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
