using System.Net;
using System.Net.Mail;

namespace E_Commerce.Services;

public class EmailService(IConfiguration configuration, ILogger<EmailService> logger) : IEmailService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly ILogger<EmailService> _logger = logger;

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        // Check if SMTP settings are configured
        var smtpHost = _configuration["Email:SmtpHost"];
        var smtpPort = _configuration.GetValue<int>("Email:SmtpPort");
        var smtpUser = _configuration["Email:SmtpUser"];
        var smtpPass = _configuration["Email:SmtpPass"];

        _logger.LogInformation("Email config: Host={Host}, Port={Port}, User={User}, HasPass={HasPass}", 
            smtpHost, smtpPort, smtpUser, !string.IsNullOrEmpty(smtpPass));

        if (string.IsNullOrEmpty(smtpHost) || smtpPort == 0 || string.IsNullOrEmpty(smtpUser))
        {
            // Development mode: Log email content
            _logger.LogWarning("SMTP not configured properly - logging email instead");
            _logger.LogInformation("================ EMAIL SENT ================");
            _logger.LogInformation($"To: {to}");
            _logger.LogInformation($"Subject: {subject}");
            _logger.LogInformation($"Body: {body}");
            _logger.LogInformation("============================================");
            return;
        }

        try
        {
            _logger.LogInformation("Attempting to send email to {To}", to);
            
            var client = new SmtpClient(smtpHost, smtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(smtpUser, smtpPass)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpUser ?? "noreply@shoestore.com", "ShoeStore"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
            _logger.LogInformation("Email sent successfully to {To}", to);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {To}: {Message}", to, ex.Message);
            throw; // Rethrow so we can see the error
        }
    }
}
