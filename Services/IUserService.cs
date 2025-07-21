using deneme.Models;

namespace deneme.Services
{
    public interface IUserService
    {
        Task<bool> ValidateUserAsync(string username, string password);
        Task<bool> RegisterUserAsync(User user);
        Task<bool> IsUsernameExistsAsync(string username);
        Task<bool> IsEmailExistsAsync(string email);
        Task<bool> ResetPasswordAsync(string email);
        Task<bool> ValidateResetTokenAsync(string token, string email);
        Task<bool> ResetPasswordWithTokenAsync(string token, string email, string newPassword);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<bool> UpdateUserAsync(User user);
    }
} 