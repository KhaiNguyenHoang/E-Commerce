using Microsoft.AspNetCore.Http;

namespace E_Commerce.Services;

public interface IFileService
{
    Task<string> UploadImageAsync(IFormFile file, string folder = "products");
    Task DeleteImageAsync(string imagePath);
    bool IsValidImage(IFormFile file);
}

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _env;
    private readonly string[] _allowedExtensions = [".jpg", ".jpeg", ".png", ".gif", ".webp"];
    private const long MaxFileSize = 5 * 1024 * 1024; // 5MB

    public FileService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> UploadImageAsync(IFormFile file, string folder = "products")
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("No file provided");

        if (!IsValidImage(file))
            throw new ArgumentException("Invalid image file");

        // Create upload directory
        var uploadPath = Path.Combine(_env.WebRootPath, "uploads", folder);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        // Generate unique filename
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadPath, fileName);

        // Save file
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Return relative URL
        return $"/uploads/{folder}/{fileName}";
    }

    public Task DeleteImageAsync(string imagePath)
    {
        if (string.IsNullOrEmpty(imagePath))
            return Task.CompletedTask;

        // Only delete if it's an uploaded file (not external URL)
        if (imagePath.StartsWith("/uploads/"))
        {
            var fullPath = Path.Combine(_env.WebRootPath, imagePath.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        return Task.CompletedTask;
    }

    public bool IsValidImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return false;

        if (file.Length > MaxFileSize)
            return false;

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!_allowedExtensions.Contains(extension))
            return false;

        // Check content type
        var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
        return allowedTypes.Contains(file.ContentType.ToLowerInvariant());
    }
}
