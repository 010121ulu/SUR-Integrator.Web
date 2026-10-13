using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SUR_Integrator.Web.Services;

namespace SUR_Integrator.Web.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Login { get; set; } = "";

        [BindProperty]
        public string Password { get; set; } = "";

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
            if (Request.Query.ContainsKey("logout"))
            {
                HttpContext.Session.Clear();
            }
        }

        public IActionResult OnPost()
        {
            ErrorMessage = null;

            if (DealService.Authenticate(Login, Password))
            {
                HttpContext.Session.SetString("UserLogin", Login);
                return RedirectToPage("/Dashboard");
            }

            ErrorMessage = "Неверный логин или пароль";
            return Page();
        }
    }
}