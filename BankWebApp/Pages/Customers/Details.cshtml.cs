using Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.APIs;
using Services.Interfaces;
using Services.ViewModels;
using Mapster;

namespace BankWebApp.Pages.Customers
{
    [Authorize(Roles = "Cashier")]
    public class DetailsModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IRandomUserService _randomUserService;
        private readonly IAccountService _accountService;
        public DetailsModel(ICustomerService customerService, IRandomUserService userService, IAccountService accountService)
        {
            _customerService = customerService;
            _randomUserService = userService;
            _accountService = accountService;
        }

        public List<AccountViewModel> Accounts { get; set; }
        public CustomerViewModel Customer { get; set; }

        [BindProperty]
        public string CustomerImageUrl { get; set; }

        [BindProperty]
        public int CustomerId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CustomerId = id;
            var result = await _customerService.GetCustomerByIdAsync(id);
            if (result.IsFailed)
                return RedirectToPage("/Customers/Customers");

            Customer = result.Value.Adapt<CustomerViewModel>();

            CustomerImageUrl = await _randomUserService.FetchFromApi(id);
                        
            var accounts = _accountService.GetAllAccountsFromCustomer(id);
            Accounts = accounts.Adapt<List<AccountViewModel>>();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Customers/Customers");
            }

            var customerDTO = _customerService.GetCustomerByIdAsync(CustomerId);
            Customer = customerDTO.Adapt<CustomerViewModel>();

            _accountService.CreateAccount(CustomerId);

            var accounts = _accountService.GetAllAccountsFromCustomer(CustomerId);
            Accounts = accounts.Adapt<List<AccountViewModel>>();
            return Page();
        }

    }
}
