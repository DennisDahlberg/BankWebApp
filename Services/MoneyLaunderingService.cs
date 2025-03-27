using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MoneyLaunderingService
    {
        private readonly BankAppDataContext _dbContext;

        public MoneyLaunderingService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<TransactionDTO>> GetSketchyTransactionsFromDate(DateOnly date)
        {
            var transactions = _dbContext.Transactions
                .Where(t => t.Date >= date && t.Amount > 15000).ToList();
            var transactionDTOs = new List<TransactionDTO>();

            foreach (var transaction in transactions)
            {
                var customerName = GetUserFromAccountId(transaction.AccountId);

                transactionDTOs.Add(new TransactionDTO()
                {
                    AccountId = transaction.AccountId,
                    Amount = transaction.Amount,
                    Date = transaction.Date,
                    TransactionId = transaction.TransactionId,
                    Firstname = customerName.Firstname,
                    Lastname = customerName.Lastname,
                });
            }
            return transactionDTOs;

        }

        public CustomerNameDTO GetUserFromAccountId(int accountId)
        {
            var user = _dbContext.Dispositions.Where(d => d.AccountId == accountId).Include(d => d.Customer).FirstOrDefault();

            return new CustomerNameDTO()
            {
                Firstname = user.Customer.Givenname,
                Lastname = user.Customer.Surname,
            };
        }


    }
}
