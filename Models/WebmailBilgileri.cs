using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using deneme.Models.Enums;

namespace deneme.Models
{
    public class WebmailBilgileri
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Mail Hizmeti Alınan Firma")]
        [StringLength(200)]
        public string? MailHizmetiAlinanFirma { get; set; }

        [Display(Name = "Güncelleme Tarihi")]
        public DateTime? GuncellemeTarihi { get; set; }

        [Display(Name = "Mail Kullanıcı Sayısı")]
        [Range(0, 99999, ErrorMessage = "Mail Kullanıcı Sayısı 0-99999 arasında olmalıdır")]
        public int? MailKullaniciSayisi { get; set; }

        [Display(Name = "Aktif Web Sitesi Var Mı?")]
        public bool AktifWebSitesiVarMi { get; set; }

        [Display(Name = "Site Omega Yazılım Tarafından Mı Yapıldı?")]
        public bool SiteOmegaYazilimTarafindanMiYapildi { get; set; }

        [Display(Name = "Domain Hizmeti Sağlayıcısı")]
        [StringLength(200)]
        public string? DomainHizmetiSaglayicisi { get; set; }

        [Display(Name = "Hosting Hizmeti Sağlayıcısı")]
        [StringLength(200)]
        public string? HostingHizmetiSaglayicisi { get; set; }

        [Display(Name = "Portal Kullanıcı Adı")]
        [StringLength(50)]
        public string? PortalKullaniciAdi { get; set; }

        [Display(Name = "Portal Şifre")]
        [StringLength(50)]
        public string? PortalSifre { get; set; }

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        // Foreign key for Musteri
        public int MusteriId { get; set; }
        
        [ForeignKey("MusteriId")]
        public virtual Musteri Musteri { get; set; } = null!;

        // NotMapped property for date string handling
        [NotMapped]
        [Display(Name = "Güncelleme Tarihi")]
        public string? GuncellemeTarihiString
        {
            get => GuncellemeTarihi?.ToString("dd.MM.yyyy");
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    GuncellemeTarihi = null;
                }
                else if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out var date))
                {
                    GuncellemeTarihi = date;
                }
            }
        }
    }
} 