using BankWebApp.Infrastructure.Paging;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankAppDataContext _dbContext;

        public TransactionService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }


        public PagedResult<Transaction> GetAllTransactionsFromCustomer(int accountId, int page)
        {
            var transactions = _dbContext.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .AsQueryable();

            return transactions.GetPaged(page, 20);
        }


        public async Task<List<SuspectTransactionDTO>> GetSuspectTransactions(DateOnly date, string country)
        {
            var transactionDTOs = await _dbContext.Transactions
                .Where(t => t.Date >= date && t.Amount > 15000)
                .Join(
                    _dbContext.Dispositions.Include(d => d.Customer),
                    transaction => transaction.AccountId,
                    disposition => disposition.AccountId,
                    (transaction, disposition) => new
                    {
                        Transaction = transaction,
                        Disposition = disposition
                    })
                .Where(t => t.Disposition.Customer.Country == country)
                .Select(t => new SuspectTransactionDTO
                {
                    AccountId = t.Transaction.AccountId,
                    Amount = t.Transaction.Amount,
                    Date = t.Transaction.Date,
                    TransactionId = t.Transaction.TransactionId,
                    Firstname = t.Disposition.Customer.Givenname,
                    Lastname = t.Disposition.Customer.Surname
                })
                .AsNoTracking()
                .ToListAsync();

            return transactionDTOs;
        }


    }
}
