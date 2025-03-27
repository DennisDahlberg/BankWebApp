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

        public async Task<List<TransactionDTO>> GetTransactionsFromDate(DateOnly date)
        {
            try
            {
                var transactionDTOs = await _dbContext.Transactions
                    .Where(t => t.Date >= date && t.Amount > 15000)
                    .Join(
                        _dbContext.Dispositions.Include(d => d.Customer),
                        transaction => transaction.AccountId,
                        disposition => disposition.AccountId,
                        (transaction, disposition) => new TransactionDTO
                        {
                            AccountId = transaction.AccountId,
                            Amount = transaction.Amount,
                            Date = transaction.Date,
                            TransactionId = transaction.TransactionId,
                            Firstname = disposition.Customer.Givenname,
                            Lastname = disposition.Customer.Surname
                        })
                    .AsNoTracking()
                    .ToListAsync();

                return transactionDTOs;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetTransactionsFromDate: {ex.Message}");
                return new List<TransactionDTO>(); // Return an empty list instead of crashing
            }
        }


    }
}
