using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using deneme.Services;
using deneme.Models.ValidationAttributes;

namespace deneme.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRateLimitService _rateLimitService;

        public LoginModel(IUserService userService, IRateLimitService rateLimitService)
        {
            _userService = userService;
            _rateLimitService = rateLimitService;
            Input = new LoginInput();
        }

        [BindProperty]
        public LoginInput Input { get; set; }

        public class LoginInput
        {
            [Required(ErrorMessage = "Ad Soyad alanı zorunludur.")]
            [StringLength(100, MinimumLength = 3, ErrorMessage = "Ad Soyad 3-100 karakter arasında olmalıdır.")]
            [SafeString(false)]
            [Display(Name = "Ad Soyad")]
            public string Username { get; set; } = string.Empty;

            [Required(ErrorMessage = "Şifre alanı zorunludur.")]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre 6-100 karakter arasında olmalıdır.")]
            [DataType(DataType.Password)]
            [Display(Name = "Şifre")]
            public string Password { get; set; } = string.Empty;

            [Display(Name = "Beni Hatırla")]
            public bool RememberMe { get; set; }
        }

        public void OnGet()
        {
            // Tüm session'ı temizle
            HttpContext.Session.Clear();
            
            // Login sayfası cache kontrolü
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate, max-age=0";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "-1";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Rate limiting kontrolü
            var clientId = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var isAllowed = await _rateLimitService.IsLoginAttemptAllowedAsync(clientId);
            if (!isAllowed)
            {
                ModelState.AddModelError(string.Empty, "Çok fazla başarısız deneme. Lütfen 15 dakika sonra tekrar deneyin.");
                return Page();
            }

            var isValid = await _userService.ValidateUserAsync(Input.Username, Input.Password);
            if (!isValid)
            {
                // Başarısız giriş denemesini kaydet
                await _rateLimitService.RecordFailedLoginAttemptAsync(clientId);
                ModelState.AddModelError(string.Empty, "Geçersiz ad soyad veya şifre.");
                return Page();
            }

            // Kullanıcı bilgilerini al
            var user = await _userService.GetUserByUsernameAsync(Input.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı bulunamadı.");
                return Page();
            }

            // Claims oluştur
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("FullName", user.FullName)
            };

            // Identity oluştur
            var identity = new ClaimsIdentity(claims, "CustomAuth");
            var principal = new ClaimsPrincipal(identity);

            // Authentication properties
            var properties = new AuthenticationProperties
            {
                IsPersistent = Input.RememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
            };

            // Kullanıcıyı authenticate et
            await HttpContext.SignInAsync("CustomAuth", principal, properties);

            // Başarılı giriş durumunda failed attempts'i temizle
            await _rateLimitService.ClearFailedLoginAttemptsAsync(clientId);

            // Session'ı da güncelle (tema için)
            HttpContext.Session.SetString("LoggedIn", "true");
            HttpContext.Session.SetString("UserName", Input.Username);

            return RedirectToPage("/Dashboard");
        }
    }
} 