using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deneme.Models
{
    public class ModemAcikPort
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Port Numarası")]
        [StringLength(50)]
        public string? PortNumarasi { get; set; }

        [Display(Name = "Protokol")]
        [StringLength(10)]
        public string? Protokol { get; set; }

        [Display(Name = "Açıklama")]
        [StringLength(200)]
        public string? Aciklama { get; set; }

        // Foreign key for ModemBilgileri
        public int ModemBilgileriId { get; set; }
        
        [ForeignKey("ModemBilgileriId")]
        public virtual ModemBilgileri ModemBilgileri { get; set; } = null!;
    }
} 