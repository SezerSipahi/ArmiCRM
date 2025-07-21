namespace deneme.Services
{
    public interface IRateLimitService
    {
        Task<bool> IsRequestAllowedAsync(string clientId, string endpoint);
        Task<bool> IsLoginAttemptAllowedAsync(string clientId);
        Task RecordFailedLoginAttemptAsync(string clientId);
        Task ClearFailedLoginAttemptsAsync(string clientId);
    }
} 