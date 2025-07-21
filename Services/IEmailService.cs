namespace deneme.Services
{
    public interface IEmailService
    {
        Task<bool> SendPasswordResetEmailAsync(string toEmail, string resetToken, string userName);
        Task<bool> SendWelcomeEmailAsync(string toEmail, string userName);
    }
} 