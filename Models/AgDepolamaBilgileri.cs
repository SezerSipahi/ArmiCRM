using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using deneme.Models.Enums;

namespace deneme.Models
{
    public class AgDepolamaBilgileri
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "RAID Tipi")]
        [StringLength(50)]
        public string? RaidTipi { get; set; }

        [Display(Name = "Disk Boyutu")]
        [Range(0, 999999, ErrorMessage = "Disk boyutu 0-999999 GB arasında olmalıdır")]
        public int? DiskBoyutu { get; set; }

        [Display(Name = "Disk Sayısı")]
        [Range(0, 999, ErrorMessage = "Disk sayısı 0-999 arasında olmalıdır")]
        public int? DiskSayisi { get; set; }

        [Display(Name = "Yedekleme Dosya Yolu")]
        [StringLength(500)]
        public string? YedeklemeDosyaYolu { get; set; }

        [Display(Name = "Yedekleme Durumu")]
        public YedeklemeDurumu? YedeklemeDurumu { get; set; }

        [Display(Name = "Son Güncelleme Tarihi")]
        public DateTime? SonGuncellemeTarihi { get; set; }

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        // Foreign key for Musteri
        public int MusteriId { get; set; }
        
        [ForeignKey("MusteriId")]
        public virtual Musteri Musteri { get; set; } = null!;

        // String property for date handling (gg.aa.yyyy format)
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