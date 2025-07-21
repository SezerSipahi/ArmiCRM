using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using deneme.Data;
using deneme.Models;
using System.Text.RegularExpressions;

namespace deneme.Pages.MusteriAgSistemi
{
    public class DuzenleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DuzenleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Musteri Musteri { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                // Eğer id null ise, yeni müşteri ekleme sayfasına yönlendir
                return RedirectToPage("./Ekle");
            }

            // Müşteriyi ve ilişkili tüm verilerini getir
            Musteri = await _context.Musteriler
                .Include(m => m.ErpBilgileri)
                    .ThenInclude(e => e.ServerIpAdresleri)
                .Include(m => m.OzelYazilimBilgileri)
                .Include(m => m.AgDepolamaBilgileri)
                .Include(m => m.GuvenlikDuvariBilgileri)
                .Include(m => m.ModemBilgileri)
                .Include(m => m.WebmailBilgileri)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Musteri == null)
            {
                return NotFound();
            }

            return Page();
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

                // Son güncelleme tarihini güncelle
                Musteri.SonGuncellemeTarihi = DateTime.Now;

                // Müşteriyi güncelle
                _context.Attach(Musteri).State = EntityState.Modified;

                // İlişkili tabloları güncelle (sadece dolu olanları)
                if (Musteri.ErpBilgileri != null && Musteri.ErpBilgileri.Any())
                {
                    foreach (var erp in Musteri.ErpBilgileri)
                    {
                        if (erp.Id == 0)
                            _context.ErpBilgileri.Add(erp);
                        else
                            _context.Attach(erp).State = EntityState.Modified;
                    }
                }
                if (Musteri.OzelYazilimBilgileri != null && Musteri.OzelYazilimBilgileri.Any())
                {
                    foreach (var yazilim in Musteri.OzelYazilimBilgileri)
                    {
                        if (yazilim.Id == 0)
                            _context.OzelYazilimBilgileri.Add(yazilim);
                        else
                            _context.Attach(yazilim).State = EntityState.Modified;
                    }
                }
                if (Musteri.AgDepolamaBilgileri != null)
                    _context.Attach(Musteri.AgDepolamaBilgileri).State = EntityState.Modified;
                if (Musteri.GuvenlikDuvariBilgileri != null)
                    _context.Attach(Musteri.GuvenlikDuvariBilgileri).State = EntityState.Modified;
                if (Musteri.ModemBilgileri != null)
                    _context.Attach(Musteri.ModemBilgileri).State = EntityState.Modified;
                if (Musteri.WebmailBilgileri != null)
                    _context.Attach(Musteri.WebmailBilgileri).State = EntityState.Modified;

                // Değişiklikleri kaydet
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu: " + ex.Message);
                return Page();
            }
        }
    }
} 