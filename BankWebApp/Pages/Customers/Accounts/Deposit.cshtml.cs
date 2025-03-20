using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Intefaces;

namespace BankWebApp.Pages.Customers.Accounts
{
    public class DepositModel : PageModel
    {
        private readonly IAccountService _accountService;

        public DepositModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public void OnGet(int accountId, int customerId)
        {
            AccountId = accountId;
            CustomerId = customerId;
            Balance = _accountService.GetAccount(accountId).Balance;
        }
    }
}
