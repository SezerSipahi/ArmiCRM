using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;

namespace deneme.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            // Authentication cookie'sini temizle
            await HttpContext.SignOutAsync("CustomAuth");
            
            // Session'ı temizle
            HttpContext.Session.Clear();
            
            // Logout flag'ini set et
            HttpContext.Session.SetString("LoggedOut", "true");
            
            // Cache kontrolü
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate, max-age=0";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "-1";
            
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            // POST isteği durumunda da aynı işlemleri yap
            return await OnGetAsync();
        }
    }
} 