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

        public List<CountryStatsViewModel> CountryStats { get; set; }

        public void OnGet()
        {
            CountryStats = _countryService.GetCountriesStats();
        }
    }
}
