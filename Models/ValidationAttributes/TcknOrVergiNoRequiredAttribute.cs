using System.ComponentModel.DataAnnotations;

namespace deneme.Models.ValidationAttributes
{
    public class TcknOrVergiNoRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return true; // Bu attribute sınıf seviyesinde çalışır
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is Musteri musteri)
            {
                // TCKN veya VergiNo'dan en az biri dolu olmalı
                if (string.IsNullOrWhiteSpace(musteri.TCKN) && string.IsNullOrWhiteSpace(musteri.VergiNo))
                {
                    return new ValidationResult("TCKN veya Vergi No'dan en az biri zorunludur.", new[] { nameof(Musteri.TCKN), nameof(Musteri.VergiNo) });
                }

                // TCKN kontrolü - 11 haneli ve sadece rakam
                if (!string.IsNullOrWhiteSpace(musteri.TCKN))
                {
                    if (musteri.TCKN.Length != 11 || !System.Text.RegularExpressions.Regex.IsMatch(musteri.TCKN, @"^\d{11}$"))
                    {
                        return new ValidationResult("TCKN 11 haneli ve sadece rakam olmalıdır.", new[] { nameof(Musteri.TCKN) });
                    }
                }

                // Vergi No kontrolü - 10 haneli ve sadece rakam
                if (!string.IsNullOrWhiteSpace(musteri.VergiNo))
                {
                    if (musteri.VergiNo.Length != 10 || !System.Text.RegularExpressions.Regex.IsMatch(musteri.VergiNo, @"^\d{10}$"))
                    {
                        return new ValidationResult("Vergi No 10 haneli ve sadece rakam olmalıdır.", new[] { nameof(Musteri.VergiNo) });
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
} 