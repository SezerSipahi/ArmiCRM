using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using deneme.Models.Enums;

namespace deneme.Models
{
    public class GuvenlikDuvariBilgileri
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Güvenlik Duvarı Var Mı?")]
        public bool GuvenlikDuvariVarMi { get; set; }

        [Display(Name = "Model")]
        [StringLength(100)]
        public string? Model { get; set; }

        [Display(Name = "Güncelleme Tarihi")]
        public DateTime? GuncellemeTarihi { get; set; }

        [Display(Name = "LEM Tarihi")]
        public DateTime? LemTarihi { get; set; }

        [Display(Name = "Yedekleme Dosya Yolu")]
        [StringLength(500)]
        public string? YedeklemeDosyaYolu { get; set; }

        [Display(Name = "Yedekleme Durumu")]
        public YedeklemeDurumu YedeklemeDurumu { get; set; }

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        public DateTime? SonGuncellemeTarihi { get; set; }

        // Foreign key for Musteri
        public int MusteriId { get; set; }
        
        [ForeignKey("MusteriId")]
        public virtual Musteri Musteri { get; set; } = null!;

        // One-to-many relationship with AcikPort
        public virtual IList<AcikPort> AcikPortlar { get; set; } = new List<AcikPort>();

        // NotMapped properties for date string handling
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

        [NotMapped]
        [Display(Name = "LEM Tarihi")]
        public string? LemTarihiString
        {
            get => LemTarihi?.ToString("dd.MM.yyyy");
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    LemTarihi = null;
                }
                else if (DateTime.TryParseExact(value, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out var date))
                {
                    LemTarihi = date;
                }
            }
        }
    }
} 