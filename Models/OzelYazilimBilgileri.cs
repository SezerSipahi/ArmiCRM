using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using deneme.Models.Enums;

namespace deneme.Models
{
    public class OzelYazilimBilgileri
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Yazılım İsmi")]
        [StringLength(200)]
        public string? YazilimIsmi { get; set; }

        [Display(Name = "Kullanıcı Sayısı")]
        [Range(0, 99999, ErrorMessage = "Kullanıcı sayısı 0-99999 arasında olmalıdır")]
        public int? KullaniciSayisi { get; set; }

        [Display(Name = "Kiralama Modeli")]
        public KiralamaModeli? KiralamaModeli { get; set; }

        [Display(Name = "Entegrasyon Durumu")]
        public bool EntegrasyonDurumu { get; set; }

        [Display(Name = "Entegrasyon Açıklama")]
        [StringLength(500)]
        public string? EntegrasyonAciklama { get; set; }

        [Display(Name = "Destek Veren Firma")]
        [StringLength(200)]
        public string? DestekVerenFirma { get; set; }

        [Display(Name = "Kurulum Tarihi")]
        public DateTime? KurulumTarihi { get; set; }

        [Display(Name = "Sürüm Bilgisi")]
        [StringLength(50)]
        public string? SurumBilgisi { get; set; }

        [Display(Name = "LEM Tarihi")]
        public DateTime? LemTarihi { get; set; }

        [Display(Name = "Güncelleme Tarihi")]
        public DateTime? GuncellemeTarihi { get; set; }

        [Display(Name = "Lisans Türü")]
        public LisansTuru? LisansTuru { get; set; }

        [Display(Name = "Notlar")]
        [StringLength(1000)]
        public string? Notlar { get; set; }

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        public DateTime? SonGuncellemeTarihi { get; set; }

        // Foreign key for Musteri
        public int MusteriId { get; set; }
        
        [ForeignKey("MusteriId")]
        public virtual Musteri Musteri { get; set; } = null!;

        // String properties for date handling (gg.aa.yyyy format)
        [NotMapped]
        [Display(Name = "Kurulum Tarihi")]
        [RegularExpression(@"^\d{2}\.\d{2}\.\d{4}$", ErrorMessage = "Kurulum Tarihi gg.aa.yyyy formatında olmalıdır")]
        public string KurulumTarihiString 
        { 
            get => KurulumTarihi?.ToString("dd.MM.yyyy") ?? string.Empty;
            set 
            {
                if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out var date))
                    KurulumTarihi = date;
                else
                    KurulumTarihi = null;
            }
        }

        [NotMapped]
        [Display(Name = "LEM Tarihi")]
        [RegularExpression(@"^\d{2}\.\d{2}\.\d{4}$", ErrorMessage = "LEM Tarihi gg.aa.yyyy formatında olmalıdır")]
        public string LemTarihiString 
        { 
            get => LemTarihi?.ToString("dd.MM.yyyy") ?? string.Empty;
            set 
            {
                if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out var date))
                    LemTarihi = date;
                else
                    LemTarihi = null;
            }
        }

        [NotMapped]
        [Display(Name = "Güncelleme Tarihi")]
        [RegularExpression(@"^\d{2}\.\d{2}\.\d{4}$", ErrorMessage = "Güncelleme Tarihi gg.aa.yyyy formatında olmalıdır")]
        public string GuncellemeTarihiString 
        { 
            get => GuncellemeTarihi?.ToString("dd.MM.yyyy") ?? string.Empty;
            set 
            {
                if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out var date))
                    GuncellemeTarihi = date;
                else
                    GuncellemeTarihi = null;
            }
        }


    }
} 