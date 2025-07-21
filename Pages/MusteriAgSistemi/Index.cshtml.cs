using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using deneme.Data;
using deneme.Models;

namespace deneme.Pages.MusteriAgSistemi
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Musteri> Musteriler { get; set; }

        public async Task OnGetAsync()
        {
            // Cache kontrolü - geri tuşunu devre dışı bırak
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate, max-age=0";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "-1";
            
            // Giriş kontrolü - kullanıcı giriş yapmış mı?
            var isLoggedIn = HttpContext.Session.GetString("LoggedIn");
            var isLoggedOut = HttpContext.Session.GetString("LoggedOut");
            
            if (isLoggedOut == "true" || isLoggedIn != "true")
            {
                // Session'ı temizle ve login sayfasına yönlendir
                HttpContext.Session.Clear();
                Response.Redirect("/Account/Login");
                return;
            }
            
            Musteriler = await _context.Musteriler
                .OrderByDescending(m => m.OlusturmaTarihi)
                .ToListAsync();
        }
    }
} 