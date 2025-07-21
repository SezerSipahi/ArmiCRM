using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deneme.Models
{
    public class ModemKullaniciErisimBilgileri
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [StringLength(50)]
        public string? KullaniciAdi { get; set; }

        [Display(Name = "Şifre")]
        [StringLength(50)]
        public string? Sifre { get; set; }

        [Display(Name = "Açıklama")]
        [StringLength(200)]
        public string? Aciklama { get; set; }

        // Foreign key for ModemBilgileri
        public int ModemBilgileriId { get; set; }
        
        [ForeignKey("ModemBilgileriId")]
        public virtual ModemBilgileri ModemBilgileri { get; set; } = null!;
    }
} 