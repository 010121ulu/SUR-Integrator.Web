using Microsoft.AspNetCore.Mvc.RazorPages;
using SUR_Integrator.Web.Services;

namespace SUR_Integrator.Web.Pages
{
    public class DashboardModel : PageModel
    {
        public List<Deal> Deals { get; set; } = new();
        public UserInfo? CurrentUser { get; set; }

        public void OnGet()
        {
            var login = HttpContext.Session.GetString("UserLogin");
            if (string.IsNullOrEmpty(login))
            {
                CurrentUser = null;
                return;
            }

            CurrentUser = DealService.GetUser(login);
            Deals = DealService.GetUserDeals(login);
        }
    }
}