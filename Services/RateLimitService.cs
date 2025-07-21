using System.Collections.Concurrent;

namespace deneme.Services
{
    public class RateLimitService : IRateLimitService
    {
        private readonly ConcurrentDictionary<string, RequestInfo> _requests = new();
        private readonly ConcurrentDictionary<string, LoginAttemptInfo> _loginAttempts = new();
        private readonly ILogger<RateLimitService> _logger;

        public RateLimitService(ILogger<RateLimitService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> IsRequestAllowedAsync(string clientId, string endpoint)
        {
            var key = $"{clientId}:{endpoint}";
            var now = DateTime.UtcNow;
            var windowSize = TimeSpan.FromMinutes(1);
            var maxRequests = 60; // 60 requests per minute

            _requests.AddOrUpdate(key, 
                new RequestInfo { Count = 1, WindowStart = now },
                (_, existing) =>
                {
                    if (now - existing.WindowStart > windowSize)
                    {
                        // Reset window
                        existing.Count = 1;
                        existing.WindowStart = now;
                    }
                    else
                    {
                        existing.Count++;
                    }
                    return existing;
                });

            var requestInfo = _requests[key];
            var isAllowed = requestInfo.Count <= maxRequests;

            if (!isAllowed)
            {
                _logger.LogWarning("Rate limit exceeded for client {ClientId} on endpoint {Endpoint}", clientId, endpoint);
            }

            return await Task.FromResult(isAllowed);
        }

        public async Task<bool> IsLoginAttemptAllowedAsync(string clientId)
        {
            var now = DateTime.UtcNow;
            var windowSize = TimeSpan.FromMinutes(15);
            var maxAttempts = 5; // 5 failed attempts per 15 minutes

            if (!_loginAttempts.TryGetValue(clientId, out var attemptInfo))
            {
                return await Task.FromResult(true);
            }

            // Clean up old attempts
            attemptInfo.FailedAttempts = attemptInfo.FailedAttempts
                .Where(attempt => now - attempt < windowSize)
                .ToList();

            var isAllowed = attemptInfo.FailedAttempts.Count < maxAttempts;

            if (!isAllowed)
            {
                _logger.LogWarning("Login rate limit exceeded for client {ClientId}. {Count} failed attempts in last 15 minutes", 
                    clientId, attemptInfo.FailedAttempts.Count);
            }

            return await Task.FromResult(isAllowed);
        }

        public async Task RecordFailedLoginAttemptAsync(string clientId)
        {
            var now = DateTime.UtcNow;
            
            _loginAttempts.AddOrUpdate(clientId,
                new LoginAttemptInfo { FailedAttempts = new List<DateTime> { now } },
                (_, existing) =>
                {
                    existing.FailedAttempts.Add(now);
                    return existing;
                });

            _logger.LogInformation("Failed login attempt recorded for client {ClientId}", clientId);
            await Task.CompletedTask;
        }

        public async Task ClearFailedLoginAttemptsAsync(string clientId)
        {
            _loginAttempts.TryRemove(clientId, out _);
            _logger.LogInformation("Failed login attempts cleared for client {ClientId}", clientId);
            await Task.CompletedTask;
        }

        private class RequestInfo
        {
            public int Count { get; set; }
            public DateTime WindowStart { get; set; }
        }

        private class LoginAttemptInfo
        {
            public List<DateTime> FailedAttempts { get; set; } = new();
        }
    }
} 