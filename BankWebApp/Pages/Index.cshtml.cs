using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.Intefaces;
using Services.ViewModels;

namespace BankWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICountryService _countryService;

        public IndexModel(ICountryService service)
        {
            _countryService = service;
        }

        public List<CountryStatsViewModel> CountryStats { get; set; }

        public void OnGet()
        {
            CountryStats = _countryService.GetCountriesStats()
                .Select(c => new CountryStatsViewModel
                {
                    AmountOfAccounts = c.AmountOfAccounts,
                    AmountOfCustomers = c.AmountOfCustomers,
                    AmountOfMoney = c.AmountOfMoney,
                    ImageUrl = c.ImageUrl
                }).ToList();
        }
    }
}
