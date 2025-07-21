using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using deneme.Models.Enums;

namespace deneme.Models
{
    public class ErpBilgileri
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "ERP Ürün Adı")]
        [StringLength(200, ErrorMessage = "ERP Ürün Adı 200 karakterden fazla olamaz")]
        public string? ErpUrunAdi { get; set; }

        [Display(Name = "Sürüm Bilgisi")]
        [StringLength(50, ErrorMessage = "Sürüm Bilgisi 50 karakterden fazla olamaz")]
        public string? SurumBilgisi { get; set; }

        [Display(Name = "Kullanıcı Sayısı")]
        [Range(0, 99999, ErrorMessage = "Kullanıcı Sayısı 0-99999 arasında olmalıdır")]
        public int? KullaniciSayisi { get; set; }

        [Display(Name = "Kullanım Şekli")]
        public KullanimSekli? KullanimSekli { get; set; }

        [Display(Name = "Lisans Türü")]
        public LisansTuru? LisansTuru { get; set; }

        [Display(Name = "Kurulum Tarihi")]
        public DateTime? KurulumTarihi { get; set; }

        [Display(Name = "LEM Tarihi")]
        public DateTime? LemTarihi { get; set; }

        [Display(Name = "Yedekleme Durumu")]
        public YedeklemeDurumu? YedeklemeDurumu { get; set; }

        [Display(Name = "Notlar")]
        [StringLength(1000, ErrorMessage = "Notlar 1000 karakterden fazla olamaz")]
        public string? Notlar { get; set; }

        [Display(Name = "Son Güncelleme Tarihi")]
        public DateTime? SonGuncellemeTarihi { get; set; }

        [Display(Name = "Oluşturma Tarihi")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        // Foreign key for Musteri
        public int MusteriId { get; set; }
        
        [ForeignKey("MusteriId")]
        public virtual Musteri Musteri { get; set; } = null!;

        // Server IP Adresleri koleksiyonu
        public virtual List<ErpServerIpAdresi> ServerIpAdresleri { get; set; } = new List<ErpServerIpAdresi>();

        // Tarih alanları için string property'ler (UI'da kullanılacak)
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
        [Display(Name = "Son Güncelleme Tarihi")]
        [RegularExpression(@"^\d{2}\.\d{2}\.\d{4}$", ErrorMessage = "Son Güncelleme Tarihi gg.aa.yyyy formatında olmalıdır")]
        public string SonGuncellemeTarihiString 
        { 
            get => SonGuncellemeTarihi?.ToString("dd.MM.yyyy") ?? string.Empty;
            set 
            {
                if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out var date))
                    SonGuncellemeTarihi = date;
                else
                    SonGuncellemeTarihi = null;
            }
        }
    }
} 