using BankWebApp.ViewModels;
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
        public string CustomerImageUrl { get; set; }

        public async Task OnGetAsync(int id)
        {
            var customer = _customerService.GetCustomerByIdAsync(id);

            Customer = new CustomerViewModel
            {
                GivenName = customer.GivenName,
                SurName = customer.SurName,
                CustomerId = customer.CustomerId,
                Gender = customer.Gender,
                Address = customer.Address,
                Country = customer.Country,
                City = customer.City,
            };

            CustomerImageUrl = await _randomUserService.FetchFromApi(id);

            Accounts = _accountService.GetAllAccountsFromCustomer(id)
                .Select(a => new AccountViewModel
                {
                    Balance = a.Balance,
                    AccountId = a.AccountId,
                   
                }).ToList();

        }
    }
}
