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

        public async Task OnGetAsync(int id)
        {
            CustomerId = id;
            var customerDTO = _customerService.GetCustomerByIdAsync(id);
                        
            Customer = new CustomerViewModel();
            Customer = customerDTO.Adapt<CustomerViewModel>();

            CustomerImageUrl = await _randomUserService.FetchFromApi(id);

            Accounts = _accountService.GetAllAccountsFromCustomer(id)
                .Select(a => new AccountViewModel
                {
                    Balance = a.Balance,
                    AccountId = a.AccountId,
                   
                }).ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Customers/Customers");
            }

            var customerDTO = _customerService.GetCustomerByIdAsync(CustomerId);
            Customer = new CustomerViewModel
            {
                Givenname = customerDTO.Givenname,
                Surname = customerDTO.Surname,
                CustomerId = customerDTO.CustomerId,
                Gender = customerDTO.Gender,
                Streetaddress = customerDTO.Streetaddress,
                Country = customerDTO.Country,
                City = customerDTO.City,
            };

            _accountService.CreateAccount(CustomerId);

            Accounts = _accountService.GetAllAccountsFromCustomer(CustomerId)
                .Select(a => new AccountViewModel
                {
                    Balance = a.Balance,
                    AccountId = a.AccountId,

                }).ToList();



            return Page();
        }

    }
}
