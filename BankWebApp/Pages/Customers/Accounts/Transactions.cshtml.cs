using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using Services.ViewModels;

namespace BankWebApp.Pages.Customers.Accounts
{
    public class TransactionsModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;

        public TransactionsModel(ITransactionService transactionService, IAccountService accountService)
        {
            _transactionService = transactionService;
            _accountService = accountService;
        }

        public List<TransactionViewModel> Transactions { get; set; }

        [BindProperty]
        public int AccountId { get; set; }

        [BindProperty]
        public int CustomerId { get; set; }

        public int CurrentPage { get; set; }

        public int PageCount { get; set; }


        public void OnGet(int accountId, int customerId)
        {
            CustomerId = customerId;
            AccountId = accountId;     
        }

        public IActionResult OnGetShowMore(int pageNo, int accountId)
        {
            var transactions = _transactionService.GetAllTransactionsFromCustomer(accountId, pageNo);
            PageCount = transactions.PageCount;
            CurrentPage = pageNo;
            var transactionViewModels = transactions.Results.Adapt<List<TransactionViewModel>>();

            return new JsonResult(new { transactions = transactionViewModels, maxPage = PageCount });
        }

        public async Task<IActionResult> OnPost()
        {
            await _accountService.Delete(AccountId);

            return RedirectToPage("/Customers/Details", new { id = CustomerId });
        }
    }
}
