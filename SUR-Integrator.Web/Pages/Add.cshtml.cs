using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SUR_Integrator.Web.Services;

namespace SUR_Integrator.Web.Pages
{
    public class AddModel : PageModel
    {
        public string? GeneratedId { get; set; }

        public void OnPost(string sender, string destination, string product, string tnvedCode, decimal value)
        {
            var deal = new Deal
            {
                Sender = sender,
                Destination = destination,
                Product = product,
                TnvedCode = string.IsNullOrWhiteSpace(tnvedCode) ? "0000 00 000 0" : tnvedCode,
                Value = value,
                DutyRate = 10,
                VatRate = 22
            };

            GeneratedId = DealService.AddDeal(deal);
        }
    }
}