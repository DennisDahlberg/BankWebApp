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

        public TransactionsModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public List<TransactionViewModel> Transactions { get; set; }
        public int AccountId { get; set; }
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

        public void OnPost()
        {

        }
    }
}
