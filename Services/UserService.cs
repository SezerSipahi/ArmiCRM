using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using deneme.Models;
using deneme.Data;

namespace deneme.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public UserService(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.FullName == username);
            if (user == null)
                return false;

            return VerifyPassword(password, user.PasswordHash);
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            // Kullanıcı adı veya email zaten var mı kontrol et
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.FullName == user.FullName || u.Email == user.Email);
            
            if (existingUser != null)
                return false;

            // Şifreyi hash'le
            user.PasswordHash = HashPassword(user.PasswordHash);
            user.CreatedAt = DateTime.Now;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            // Hoş geldin e-postası gönder
            try
            {
                await _emailService.SendWelcomeEmailAsync(user.Email, user.FullName);
            }
            catch (Exception ex)
            {
                // E-posta gönderim hatası log'lanır ama kayıt işlemi başarılı sayılır
                Console.WriteLine($"Hoş geldin e-postası gönderilirken hata: {ex.Message}");
            }
            
            return true;
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsUsernameExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.FullName == username);
        }

        public async Task<bool> ResetPasswordAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return false;

            // Önceki kullanılmamış token'ları pasif yap
            var existingTokens = await _context.PasswordResetTokens
                .Where(t => t.Email == email && !t.IsUsed && t.ExpiresAt > DateTime.Now)
                .ToListAsync();
            
            foreach (var token in existingTokens)
            {
                token.IsUsed = true;
                token.UsedAt = DateTime.Now;
            }

            // Yeni reset token oluştur
            var resetToken = GenerateResetToken();
            var passwordResetToken = new PasswordResetToken
            {
                Token = resetToken,
                Email = email,
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddHours(1), // 1 saat geçerli
                IsUsed = false
            };

            _context.PasswordResetTokens.Add(passwordResetToken);
            await _context.SaveChangesAsync();

            // Şifre sıfırlama e-postası gönder
            try
            {
                var result = await _emailService.SendPasswordResetEmailAsync(email, resetToken, user.FullName);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Şifre sıfırlama e-postası gönderilirken hata: {ex.Message}");
                return false;
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            var hashedPassword = HashPassword(password);
            return hashedPassword == hash;
        }

        public async Task<bool> ValidateResetTokenAsync(string token, string email)
        {
            var resetToken = await _context.PasswordResetTokens
                .FirstOrDefaultAsync(t => t.Token == token && 
                                          t.Email == email && 
                                          !t.IsUsed && 
                                          t.ExpiresAt > DateTime.Now);
            
            return resetToken != null;
        }

        public async Task<bool> ResetPasswordWithTokenAsync(string token, string email, string newPassword)
        {
            var resetToken = await _context.PasswordResetTokens
                .FirstOrDefaultAsync(t => t.Token == token && 
                                          t.Email == email && 
                                          !t.IsUsed && 
                                          t.ExpiresAt > DateTime.Now);

            if (resetToken == null)
                return false;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return false;

            // Şifreyi güncelle
            user.PasswordHash = HashPassword(newPassword);

            // Token'ı kullanılmış olarak işaretle
            resetToken.IsUsed = true;
            resetToken.UsedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        private string GenerateResetToken()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[32];
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.FullName == username);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 