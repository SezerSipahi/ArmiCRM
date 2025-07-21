using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deneme.Models
{
    public class ErpServerIpAdresi
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "IP Adresi zorunludur")]
        [StringLength(45, ErrorMessage = "IP Adresi 45 karakterden fazla olamaz")]
        [Display(Name = "IP Adresi")]
        public string IpAdresi { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Açıklama 100 karakterden fazla olamaz")]
        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; }

        // Foreign key for ErpBilgileri
        public int ErpBilgileriId { get; set; }
        
        [ForeignKey("ErpBilgileriId")]
        public virtual ErpBilgileri ErpBilgileri { get; set; } = null!;
    }
} 