using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace deneme.Models.ValidationAttributes
{
    public class SafeStringAttribute : ValidationAttribute
    {
        private readonly bool _allowSpecialChars;

        public SafeStringAttribute(bool allowSpecialChars = false)
        {
            _allowSpecialChars = allowSpecialChars;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var stringValue = value.ToString();

            // Boş string kontrolü
            if (string.IsNullOrWhiteSpace(stringValue))
                return ValidationResult.Success;

            // XSS saldırılarını engelle
            var xssPatterns = new[]
            {
                @"<script.*?>.*?</script>",
                @"javascript:",
                @"vbscript:",
                @"onload=",
                @"onerror=",
                @"onclick=",
                @"onmouseover=",
                @"<iframe",
                @"<object",
                @"<embed",
                @"<link",
                @"<meta",
                @"<form",
                @"<input",
                @"<body",
                @"<html",
                @"<head",
                @"eval\(",
                @"expression\(",
                @"url\(",
                @"@import",
                @"&#x",
                @"&#",
                @"%3c",
                @"%3e",
                @"%22",
                @"%27"
            };

            foreach (var pattern in xssPatterns)
            {
                if (Regex.IsMatch(stringValue, pattern, RegexOptions.IgnoreCase))
                {
                    return new ValidationResult("Güvenlik nedeniyle geçersiz karakterler tespit edildi.");
                }
            }

            // SQL injection saldırılarını engelle
            var sqlPatterns = new[]
            {
                @"(\s|^)(select|insert|update|delete|drop|create|alter|exec|execute|union|declare|cast|set)(\s|$)",
                @"(\s|^)(or|and)(\s|$).*(=|<|>)",
                @"'.*?'",
                @"--;",
                @"/\*.*?\*/",
                @"xp_",
                @"sp_",
                @"0x[0-9a-f]+",
                @"char\(",
                @"nchar\(",
                @"varchar\(",
                @"nvarchar\(",
                @"@@"
            };

            foreach (var pattern in sqlPatterns)
            {
                if (Regex.IsMatch(stringValue, pattern, RegexOptions.IgnoreCase))
                {
                    return new ValidationResult("Güvenlik nedeniyle geçersiz karakterler tespit edildi.");
                }
            }

            // Özel karakterler kontrolü
            if (!_allowSpecialChars)
            {
                var allowedPattern = @"^[a-zA-Z0-9çğıöşüÇĞIİÖŞÜ\s@._-]*$";
                if (!Regex.IsMatch(stringValue, allowedPattern))
                {
                    return new ValidationResult("Sadece harf, rakam ve temel noktalama işaretleri kullanılabilir.");
                }
            }

            return ValidationResult.Success;
        }
    }
} 