using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.ViewModels;

namespace BankWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CountryService _countryService;

        public IndexModel(CountryService service)
        {
            _countryService = service;
        }

        public CountryStatsViewModel SwedenStats { get; set; }

        public void OnGet()
        {
            SwedenStats = _countryService.GetSwedenStats();
        }
    }
}
