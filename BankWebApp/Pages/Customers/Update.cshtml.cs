using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Intefaces;

namespace BankWebApp.Pages.Customers
{
    [Authorize(Roles = "Cashier")]
    public class UpdateModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly ICountryService _countryService;

        public UpdateModel(ICustomerService customerService, ICountryService countryService)
        {
            _customerService = customerService;
            _countryService = countryService;
        }

        [BindProperty]
        public CreateCustomerViewModel Customer { get; set; }

        public List<SelectListItem> Countries { get; set; }

        public void OnGet()
        {
            Countries = _countryService.GetCountryEnums();
            //var customerDTO = 
        }

    }
}
