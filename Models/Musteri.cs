using System;
using System.ComponentModel.DataAnnotations;
using deneme.Models.ValidationAttributes;

namespace deneme.Models
{
    [TcknOrVergiNoRequired]
    public class Musteri
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Firma adı zorunludur")]
        [Display(Name = "Firma Adı")]
        [StringLength(200, ErrorMessage = "Firma adı en fazla 200 karakter olabilir")]
        public string FirmaUnvani { get; set; } = string.Empty;

        [Display(Name = "TCKN")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TCKN 11 haneli olmalıdır")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "TCKN sadece rakam içermelidir")]
        public string? TCKN { get; set; }

        [Display(Name = "Vergi No")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Vergi No 10 haneli olmalıdır")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Vergi No sadece rakam içermelidir")]
        public string? VergiNo { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur")]
        [Display(Name = "Telefon")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Telefon numarası 10 haneli olmalıdır")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Telefon numarası sadece rakam içermelidir")]
        public string Telefon { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [Display(Name = "E-posta")]
        [StringLength(100, ErrorMessage = "E-posta adresi en fazla 100 karakter olabilir")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres zorunludur")]
        [Display(Name = "Adres")]
        [StringLength(500, ErrorMessage = "Adres en fazla 500 karakter olabilir")]
        public string Adres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ad zorunludur")]
        [Display(Name = "Ad")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir")]
        public string Ad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad zorunludur")]
        [Display(Name = "Soyad")]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir")]
        public string Soyad { get; set; } = string.Empty;

        [Display(Name = "Notlar")]
        [StringLength(1000, ErrorMessage = "Notlar en fazla 1000 karakter olabilir")]
        public string? Notlar { get; set; }

        [Display(Name = "Oluşturma Tarihi")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        [Display(Name = "Son Güncelleme Tarihi")]
        public DateTime? SonGuncellemeTarihi { get; set; }

        [Display(Name = "Aktiflik Durumu")]
        public bool AktifMi { get; set; } = true;

        // Navigation Properties
        public virtual List<ErpBilgileri> ErpBilgileri { get; set; } = new List<ErpBilgileri>();
        public virtual List<OzelYazilimBilgileri> OzelYazilimBilgileri { get; set; } = new List<OzelYazilimBilgileri>();
        public virtual List<OzelParametre> OzelParametreler { get; set; } = new List<OzelParametre>();
        public virtual AgDepolamaBilgileri? AgDepolamaBilgileri { get; set; }
        public virtual GuvenlikDuvariBilgileri? GuvenlikDuvariBilgileri { get; set; }
        public virtual ModemBilgileri? ModemBilgileri { get; set; }
        public virtual WebmailBilgileri? WebmailBilgileri { get; set; }
    }
} 