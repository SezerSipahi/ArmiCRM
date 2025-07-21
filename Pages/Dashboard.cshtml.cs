using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace deneme.Pages
{
    public class DashboardModel : PageModel
    {
        public IActionResult OnGet()
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
                return RedirectToPage("/Account/Login");
            }
            
            // Dashboard sayfasını göster
            return Page();
        }
    }
} 