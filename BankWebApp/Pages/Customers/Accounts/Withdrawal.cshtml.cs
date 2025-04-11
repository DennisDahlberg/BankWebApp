using DataAccessLayer.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace BankWebApp.Pages.Customers.Accounts
{
    [Authorize(Roles = "Cashier")]
    [BindProperties]
    public class WithdrawalModel : PageModel
    {
        private readonly IAccountService _accountService;

        public WithdrawalModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public decimal Withdrawal { get; set; }


        public async Task<IActionResult> OnGet(int accountId, int customerId)
        {
            if (!await _accountService.IsValid(accountId, customerId))
                return RedirectToPage("/Customers/Customers");

            AccountId = accountId;
            CustomerId = customerId;
            Balance = _accountService.GetAccount(accountId).Value.Balance;

            return Page();
        }

        public IActionResult OnPost()
        {
            var resultCode = _accountService.Withdrawal(AccountId, Withdrawal);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (resultCode == ResultCode.BalanceToLow)
            {
                ModelState.AddModelError("Withdrawal", "You do not have enough money for the withdrawal!");
                return Page();
            }

            return RedirectToPage("/Customers/Details", new { id = CustomerId });
        }
    }
}
