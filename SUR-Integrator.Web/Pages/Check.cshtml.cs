using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SUR_Integrator.Web.Services;

namespace SUR_Integrator.Web.Pages
{
    public class CheckModel : PageModel
    {
        [BindProperty]
        public string? SearchId { get; set; }

        public Deal? ResultDeal { get; set; }
        public string? ErrorMessage { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            ResultDeal = null;
            ErrorMessage = null;

            if (string.IsNullOrWhiteSpace(SearchId))
            {
                ErrorMessage = "Пожалуйста, введите ID сделки.";
                return Page();
            }

            var deal = DealService.FindById(SearchId.Trim());

            if (deal == null)
            {
                ErrorMessage = $"Сделка с ID «{SearchId}» не найдена в базе данных СВХ.";
                return Page();
            }

            ResultDeal = deal;
            return Page();
        }
    }
}