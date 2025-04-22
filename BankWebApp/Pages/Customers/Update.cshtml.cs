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

        public async Task<IActionResult> OnGet(int customerId)
        {
            CustomerId = customerId;
            Countries = _countryService.GetCountryEnums();
            var result = await _customerService.GetCreateCustomer(customerId);

            if (result.IsFailed)
                return RedirectToPage("/Customers/Customers");

            Customer = result.Value.Adapt<CreateCustomerViewModel>();
            return Page();
        }



        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Countries = _countryService.GetCountryEnums();
                return Page();
            }           

            var customerDTO = Customer.Adapt<CreateCustomerDTO>();

            await _customerService.UpdateCustomer(CustomerId, customerDTO);
            return RedirectToPage("/Customers/Details", new { id = CustomerId });
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var result = await _customerService.Delete(CustomerId);

            if (result.IsFailed)
                return RedirectToPage("/Index");

            return RedirectToPage("/Customers/Customers");
        }

    }
}
