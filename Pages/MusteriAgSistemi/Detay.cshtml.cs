using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using deneme.Data;
using deneme.Models;

namespace deneme.Pages.MusteriAgSistemi
{
    public class DetayModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetayModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Musteri Musteri { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Musteri = await _context.Musteriler
                .Include(m => m.ErpBilgileri)
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
    }
} 