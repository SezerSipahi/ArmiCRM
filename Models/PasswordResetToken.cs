using System.ComponentModel.DataAnnotations;

namespace deneme.Models
{
    public class PasswordResetToken
    {
        public int Id { get; set; }
        
        [Required]
        public string Token { get; set; } = string.Empty;
        
        [Required]
        public string Email { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime ExpiresAt { get; set; }
        
        public bool IsUsed { get; set; } = false;
        
        public DateTime? UsedAt { get; set; }
    }
} 