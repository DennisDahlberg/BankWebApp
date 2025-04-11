using DataAccessLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace BankWebApp.Pages.Customers.Accounts
{
    [BindProperties]
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
        public decimal TransferAmount { get; set; }
        public int CustomerId { get; set; }

        public async Task<IActionResult> OnGet(int accountIdTo, int accountIdFrom, int customerId)
        {
            if (!await _accountService.IsValid(accountIdFrom, customerId))
                return RedirectToPage("/Customers/Customers");
            CustomerId = customerId;
            AccountFromId = accountIdFrom;
            AccountToId = accountIdTo;
            AccountFromBalance = _accountService.GetAccount(AccountFromId).Value.Balance;
            var result = _accountService.GetAccount(AccountToId);
            if (result.IsFailed)
                return RedirectToPage("SelectAccount", new { customerId = customerId, accountId = accountIdFrom });
            AccountToBalance = result.Value.Balance;
            return Page();
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = _accountService.Transfer(AccountFromId, AccountToId, TransferAmount);

            if (result == ResultCode.BalanceToLow)
            {
                ModelState.AddModelError("TransferAmount", "The account does not have enough money for the transfer!");
                return Page();
            }

            return RedirectToPage("/Customers/Details", new { id = CustomerId });
        }
    }
}
