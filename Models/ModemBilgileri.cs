using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using deneme.Models.Enums;

namespace deneme.Models
{
    public class ModemBilgileri
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Modem Markası")]
        [StringLength(100)]
        public string? Marka { get; set; }

        [Display(Name = "Modem Modeli")]
        [StringLength(100)]
        public string? Model { get; set; }

        [Display(Name = "Yedekleme Dosya Yolu")]
        [StringLength(500)]
        public string? YedeklemeDosyaYolu { get; set; }

        [Display(Name = "Yedekleme Durumu")]
        public YedeklemeDurumu YedeklemeDurumu { get; set; }

        [Display(Name = "Son Güncelleme Tarihi")]
        public DateTime? SonGuncellemeTarihi { get; set; }

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        // Foreign key for Musteri
        public int MusteriId { get; set; }
        
        [ForeignKey("MusteriId")]
        public virtual Musteri Musteri { get; set; } = null!;

        // One-to-many relationships
        public virtual IList<ModemIpAdresi> IpAdresleri { get; set; } = new List<ModemIpAdresi>();
        public virtual IList<ModemAcikPort> AcikPortlar { get; set; } = new List<ModemAcikPort>();
        public virtual IList<ModemKullaniciErisimBilgileri> KullaniciErisimBilgileri { get; set; } = new List<ModemKullaniciErisimBilgileri>();

        // NotMapped property for date string handling
        [NotMapped]
        [Display(Name = "Son Güncelleme Tarihi")]
        public string? SonGuncellemeTarihiString
        {
            get => SonGuncellemeTarihi?.ToString("dd.MM.yyyy");
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    SonGuncellemeTarihi = null;
                }
                else if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out var date))
                {
                    SonGuncellemeTarihi = date;
                }
            }
        }
    }
} 