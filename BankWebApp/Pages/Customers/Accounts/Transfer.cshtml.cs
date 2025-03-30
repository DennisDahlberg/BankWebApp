using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace BankWebApp.Pages.Customers.Accounts
{
    [Authorize(Roles = "Cashier")]
    public class TransferModel : PageModel
    {
        private readonly IAccountService _accountService;

        public TransferModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public int AccountFromId { get; set; }
        public int AccountToId { get; set; }
        public decimal AccountFromBalance { get; set; }
        public decimal AccountToBalance { get; set; }
        public void OnGet(int accountIdTo, int accountIdFrom)
        {
            AccountFromId = accountIdFrom;
            AccountToId = accountIdTo;
            AccountFromBalance = _accountService.GetAccount(AccountFromId).Balance;
            AccountToBalance = _accountService.GetAccount(AccountToId).Balance;


        }
    }
}
