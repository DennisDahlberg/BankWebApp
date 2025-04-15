using DataAccessLayer.Enums;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using Services.ViewModels;

namespace BankWebApp.Pages
{
    public class TopEarnersModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public TopEarnersModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public List<TopEarnerViewModel> TopEarners { get; set; }

        public async Task<IActionResult> OnGet(Country country)
        {
            var result = await _customerService.GetTopEarners(country);

            if (result.IsFailed)
                return RedirectToPage("/Index");

            TopEarners = result.Value.Adapt<List<TopEarnerViewModel>>();
            return Page();
        }
    }
}
