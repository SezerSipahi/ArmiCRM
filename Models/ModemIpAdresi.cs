using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deneme.Models
{
    public class ModemIpAdresi
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "IP Adresi")]
        [StringLength(15)]
        public string? IpAdresi { get; set; }

        [Display(Name = "Tip")]
        [StringLength(20)]
        public string? Tip { get; set; } // "Varsayılan" veya "Güncel"

        [Display(Name = "Açıklama")]
        [StringLength(200)]
        public string? Aciklama { get; set; }

        // Foreign key for ModemBilgileri
        public int ModemBilgileriId { get; set; }
        
        [ForeignKey("ModemBilgileriId")]
        public virtual ModemBilgileri ModemBilgileri { get; set; } = null!;
    }
} 