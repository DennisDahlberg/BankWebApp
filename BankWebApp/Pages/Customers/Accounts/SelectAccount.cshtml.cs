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
        public List<AccountDTO> Accounts { get; set; }

        public void OnGet(int accountId, int customerId)
        {
            AccountId = accountId;
            Accounts = _accountService.GetAllAccounsFromCustomerExcludingOne(accountId, customerId);
        }
    }
}
