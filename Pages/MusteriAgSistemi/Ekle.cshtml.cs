using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using deneme.Data;
using deneme.Models;
using System.Text.RegularExpressions;

namespace deneme.Pages.MusteriAgSistemi
{
    public class EkleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EkleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Musteri Musteri { get; set; }

        public void OnGet()
        {
            Musteri = new Musteri
            {
                OlusturmaTarihi = DateTime.Now,
                AktifMi = true,
                // İlişkili nesneleri başlat
                ErpBilgileri = new List<ErpBilgileri>(),
                OzelYazilimBilgileri = new List<OzelYazilimBilgileri>(),
                AgDepolamaBilgileri = new AgDepolamaBilgileri(),
                GuvenlikDuvariBilgileri = new GuvenlikDuvariBilgileri(),
                ModemBilgileri = new ModemBilgileri(),
                WebmailBilgileri = new WebmailBilgileri()
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // Sadece temel müşteri bilgilerini kontrol et
                
                // TCKN veya VergiNo'dan en az biri zorunlu
                if (string.IsNullOrWhiteSpace(Musteri.TCKN) && string.IsNullOrWhiteSpace(Musteri.VergiNo))
                {
                    ModelState.AddModelError("", "TCKN veya Vergi No alanlarından en az biri doldurulmalıdır.");
                }

                // TCKN kontrolü - 11 haneli ve sadece rakam
                if (!string.IsNullOrWhiteSpace(Musteri.TCKN))
                {
                    if (Musteri.TCKN.Length != 11)
                    {
                        ModelState.AddModelError("Musteri.TCKN", "TCKN 11 haneli olmalıdır.");
                    }
                    else if (!Regex.IsMatch(Musteri.TCKN, @"^\d{11}$"))
                    {
                        ModelState.AddModelError("Musteri.TCKN", "TCKN sadece rakam içermelidir.");
                    }
                }

                // Vergi No kontrolü - 10 haneli ve sadece rakam
                if (!string.IsNullOrWhiteSpace(Musteri.VergiNo))
                {
                    if (Musteri.VergiNo.Length != 10)
                    {
                        ModelState.AddModelError("Musteri.VergiNo", "Vergi No 10 haneli olmalıdır.");
                    }
                    else if (!Regex.IsMatch(Musteri.VergiNo, @"^\d{10}$"))
                    {
                        ModelState.AddModelError("Musteri.VergiNo", "Vergi No sadece rakam içermelidir.");
                    }
                }

                // Telefon kontrolü - 10 haneli ve sadece rakam
                if (!string.IsNullOrWhiteSpace(Musteri.Telefon))
                {
                    if (Musteri.Telefon.Length != 10)
                    {
                        ModelState.AddModelError("Musteri.Telefon", "Telefon numarası 10 haneli olmalıdır.");
                    }
                    else if (!Regex.IsMatch(Musteri.Telefon, @"^\d{10}$"))
                    {
                        ModelState.AddModelError("Musteri.Telefon", "Telefon numarası sadece rakam içermelidir.");
                    }
                }

                // Sadece temel müşteri bilgilerinin model validasyonu
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                // Müşteriyi ekle
                await _context.Musteriler.AddAsync(Musteri);
                
                // Değişiklikleri kaydet
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu: " + ex.Message);
                return Page();
            }
        }
    }
} 