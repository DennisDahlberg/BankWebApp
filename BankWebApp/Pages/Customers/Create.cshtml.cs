using Services.ViewModels;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;

namespace BankWebApp.Pages.Customers
{
    [Authorize(Roles = "Cashier")]
    public class CreateModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly ICountryService _countryService;

        public CreateModel(ICustomerService customerService, ICountryService countryService)
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
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Countries = _countryService.GetCountryEnums();
                return Page();
            }

            var customerDTO = new CreateCustomerDTO()
            {
                Givenname = Customer.Givenname,
                Surname = Customer.Surname,
                Gender = Customer.Gender,
                Streetaddress = Customer.Streetaddress,
                City = Customer.City,
                Country = Customer.Country,
                Emailaddress = Customer.Emailaddress,
                Phonenumber = Customer.Phonenumber,
                Zipcode = Customer.Zipcode,
            };

            _customerService.CreateCustomerWithAccount(customerDTO);
            return RedirectToPage("/Customers/Customers");
        }
    }
}
