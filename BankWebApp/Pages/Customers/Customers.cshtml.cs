using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Intefaces;
using Services.ViewModels;

namespace BankWebApp.Pages.Customers
{
    [Authorize(Roles="Cashier")]
    public class CustomersModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomersModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public List<CustomerViewModel> Customers { get; set; }


        public void OnGetAsync(string sortBy, string sortOrder)
        {
            Customers = _customerService.GetAllCustomersAsync(sortBy, sortOrder)
                .Select(c => new CustomerViewModel
                {
                    CustomerId = c.CustomerId,
                    GivenName = c.GivenName,
                    SurName = c.SurName,
                    Gender = c.Gender,
                    Address = c.Address,
                    City = c.City,
                    Country = c.Country,
                }).ToList();
        }
    }
}
