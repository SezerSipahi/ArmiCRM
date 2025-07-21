using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using deneme.Models.Enums;

namespace deneme.Models
{
    public class OzelParametre
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Parametre Türü")]
        [Required(ErrorMessage = "Parametre Türü zorunludur")]
        public ParametreTuru ParametreTuru { get; set; }

        [Display(Name = "Parametre Adı")]
        [Required(ErrorMessage = "Parametre Adı zorunludur")]
        [StringLength(200)]
        public string ParametreAdi { get; set; } = string.Empty;

        [Display(Name = "Açıklama")]
        [StringLength(500)]
        public string? Aciklama { get; set; }

        [Display(Name = "İçerik")]
        [StringLength(1000)]
        public string? Icerik { get; set; }

        [Display(Name = "Dosya Yolu")]
        [StringLength(500)]
        public string? DosyaYolu { get; set; }

        [Display(Name = "Logo/Resim")]
        public byte[]? LogoResim { get; set; }

        [Display(Name = "Durum")]
        public bool AktifMi { get; set; } = true;

        [Display(Name = "Versiyon")]
        [StringLength(50)]
        public string? Versiyon { get; set; }

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        // Foreign key for Musteri
        public int MusteriId { get; set; }
        
        [ForeignKey("MusteriId")]
        public virtual Musteri Musteri { get; set; } = null!;
    }
} 