using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Intefaces;
using Services.ViewModels;

namespace BankWebApp.Pages.Customers
{
    public class CustomersModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomersModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public List<CustomerViewModel> Customers { get; set; }


        public async Task OnGetAsync(string sortBy, string sortOrder)
        {
            Customers = await _customerService.GetAllCustomersAsync(sortBy, sortOrder);
        }
    }
}
