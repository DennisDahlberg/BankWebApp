using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BankWebApp.Pages.Customers.Accounts
{
    [Authorize(Roles = "Cashier")]
    [BindProperties]
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

        [Range(100, double.MaxValue, ErrorMessage = "You cant make a deposit under 100kr")]
        public decimal Deposit { get; set; }

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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _accountService.Deposit(AccountId, Deposit);

            return RedirectToPage("/Customers/Details", new { id = CustomerId });
        }
    }
}
