using BankWebApp.Infrastructure.Paging;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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


        public async Task<List<SuspiciousTransactionDTO>> GetSuspiciousTransactions()
        {     
            var transactions = await _dbContext.Transactions
                .Where(t => t.Amount >= 15000 &&
                            t.Date == DateOnly.FromDateTime(DateTime.Today) ||
                            t.Date == DateOnly.FromDateTime(DateTime.Now.AddDays(-1)))
                .Join(_dbContext.Accounts,
                    t => t.AccountId,
                    a => a.AccountId,
                    (t, a) => new { t, a })
                .Join(_dbContext.Dispositions,
                    ta => ta.a.AccountId,
                    d => d.AccountId,
                    (ta, d) => new {ta.t, ta.a, d})
                .Join(_dbContext.Customers,
                    tad => tad.d.CustomerId,
                    c => c.CustomerId,
                    (tad, c) => new SuspiciousTransactionDTO()
                    {
                        AccountId = tad.a.AccountId,
                        CustomerId = c.CustomerId,
                        Amount = tad.t.Amount,
                        TransactionId = tad.t.TransactionId,
                        Givenname = c.Givenname,
                        Surname = c.Surname
                    })
                .ToListAsync();

            return transactions;
        }


    }
}
