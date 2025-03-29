using BankWebApp.ViewModels;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using Services.Interfaces;

namespace BankWebApp.Pages.Customers.Accounts
{
    [Authorize(Roles = "Cashier")]
    public class SelectAccountModel : PageModel
    {
        private readonly IAccountService _accountService;

        public SelectAccountModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public List<AccountWithCustomerNameViewModel> CustomerAccounts { get; set; }
        public List<AccountWithCustomerNameViewModel> AllAccounts { get; set; }

        public void OnGet(int accountId, int customerId)
        {
            AccountId = accountId;
            CustomerAccounts = _accountService.GetAllAccounsFromCustomerExcludingOne(accountId, customerId)
                .Select(a => new AccountWithCustomerNameViewModel()
                {
                    AccountId = a.AccountId,
                    Balance = a.Balance,
                    Firstname = a.Firstname,
                    Lastname = a.Lastname,                    
                }).ToList();

            AllAccounts = _accountService.GetAllAccounsFromAllCustomerExcludingOne(customerId)
                .Select(a => new AccountWithCustomerNameViewModel()
                {
                    AccountId = a.AccountId,
                    Balance = a.Balance,
                    Firstname = a.Firstname,
                    Lastname = a.Lastname,
                }).ToList();
        }
    }
}
