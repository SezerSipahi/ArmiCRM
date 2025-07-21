using Microsoft.AspNetCore.Mvc;
using deneme.Models;
using deneme.Services;
using System.ComponentModel.DataAnnotations;

namespace deneme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUsernameExists = await _userService.IsUsernameExistsAsync(model.Username);
            if (isUsernameExists)
            {
                return BadRequest("Bu ad soyad zaten kullanımda.");
            }

            var isEmailExists = await _userService.IsEmailExistsAsync(model.Email);
            if (isEmailExists)
            {
                return BadRequest("Bu e-posta adresi zaten kullanımda.");
            }

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                FullName = model.FullName,
                PasswordHash = model.Password // UserService'te hash'lenecek
            };

            var result = await _userService.RegisterUserAsync(user);
            if (result)
            {
                return Ok();
            }

            return BadRequest("Kayıt işlemi başarısız oldu.");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.ResetPasswordAsync(model.Email);
            if (result)
            {
                return Ok();
            }

            return BadRequest("Bu e-posta adresiyle kayıtlı bir kullanıcı bulunamadı.");
        }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Ad Soyad gereklidir")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta adresi gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kullanıcı adı gereklidir")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre gereklidir")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
        public string Password { get; set; } = string.Empty;
    }

    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "E-posta adresi gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; } = string.Empty;
    }
} 