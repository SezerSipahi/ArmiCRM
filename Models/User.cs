using System.ComponentModel.DataAnnotations;

namespace deneme.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad Soyad gereklidir")]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email adresi gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kullanıcı adı gereklidir")]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        // Veritabanında hash'lenmiş şifre saklanacak
        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
} 