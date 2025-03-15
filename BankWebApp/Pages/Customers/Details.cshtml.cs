using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.APIs;
using Services.Intefaces;
using Services.ViewModels;

namespace BankWebApp.Pages.Customers
{
    [Authorize(Roles = "Cashier")]
    public class DetailsModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly RandomUserService _randomUserService;
        public DetailsModel(ICustomerService customerService, RandomUserService userService)
        {
            _customerService = customerService;
            _randomUserService = userService;
        }

        public CustomerViewModel Customer { get; set; }
        public string CustomerImageUrl { get; set; }

        public async Task OnGetAsync(int id)
        {
            Customer = await _customerService.GetCustomerByIdAsync(id);
            CustomerImageUrl = await _randomUserService.FetchFromApi(id);
        }
    }
}
