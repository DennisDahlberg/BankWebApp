using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankWebApp.Pages.Customers.Accounts
{
    public class DepositModel : PageModel
    {




        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public void OnGet(int accountId, int customerId)
        {
            AccountId = accountId;
            CustomerId = customerId;
        }
    }
}
