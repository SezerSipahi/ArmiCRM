using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using deneme.Services;

namespace deneme.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IUserService _userService;

        public ResetPasswordModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string Token { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Yeni şifre gerekli")]
        [StringLength(100, ErrorMessage = "Şifre en az {2} karakter olmalıdır", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Şifre tekrarı gerekli")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Şifreler eşleşmiyor")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public bool IsValidToken { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = false;

        public async Task<IActionResult> OnGetAsync(string token, string email)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
            {
                Message = "Geçersiz şifre sıfırlama bağlantısı.";
                IsSuccess = false;
                return Page();
            }

            Token = token;
            Email = email;

            // Token'ı doğrula
            IsValidToken = await _userService.ValidateResetTokenAsync(token, email);

            if (!IsValidToken)
            {
                Message = "Şifre sıfırlama bağlantısı geçersiz veya süresi dolmuş.";
                IsSuccess = false;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                IsValidToken = await _userService.ValidateResetTokenAsync(Token, Email);
                return Page();
            }

            // Token'ı tekrar doğrula
            if (!await _userService.ValidateResetTokenAsync(Token, Email))
            {
                Message = "Şifre sıfırlama bağlantısı geçersiz veya süresi dolmuş.";
                IsSuccess = false;
                IsValidToken = false;
                return Page();
            }

            // Şifreyi sıfırla
            var result = await _userService.ResetPasswordWithTokenAsync(Token, Email, NewPassword);

            if (result)
            {
                Message = "Şifreniz başarıyla güncellendi. Yeni şifrenizle giriş yapabilirsiniz.";
                IsSuccess = true;
                IsValidToken = false;
            }
            else
            {
                Message = "Şifre güncellenirken bir hata oluştu. Lütfen tekrar deneyiniz.";
                IsSuccess = false;
                IsValidToken = true;
            }

            return Page();
        }
    }
} 