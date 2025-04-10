using Services.ViewModels;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;
using Mapster;

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

        [BindProperty]
        public int CustomerId { get; set; }
        public List<SelectListItem> Countries { get; set; }

        public void OnGet(int customerId)
        {
            CustomerId = customerId;
            Countries = _countryService.GetCountryEnums();
            var customerDTO = _customerService.GetCreateCustomer(customerId);
            Customer = customerDTO.Adapt<CreateCustomerViewModel>();            
        }



        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Countries = _countryService.GetCountryEnums();
                return Page();
            }           

            var customerDTO = Customer.Adapt<CreateCustomerDTO>();

            _customerService.UpdateCustomer(CustomerId, customerDTO);
            return RedirectToPage("/Customers/Details", new { id = CustomerId });
        }

        public void OnPostDelete()
        {

        }

    }
}
