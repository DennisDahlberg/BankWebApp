using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Services.Enums;
using Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly BankAppDataContext _dbContext;

        public AccountService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<AccountDTO> GetAllAccountsFromCustomer(int customerId)
        {
            var accounts = _dbContext.Accounts.Where(a => a.Dispositions.Any(d => d.CustomerId == customerId));

            return accounts.Select(a => new AccountDTO
            {
                Balance = a.Balance,
                AccountId = a.AccountId,
            }).ToList();
        }

        public AccountDTO GetAccount(int accountId)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.AccountId == accountId);

            return new AccountDTO()
            {
                AccountId = account.AccountId,
                Balance = account.Balance,
            };
        }

        public void Deposit(int accountId, decimal amount)
        {
            var account = _dbContext.Accounts.First(a => a.AccountId == accountId);
            account.Balance += amount;

            CreateTransanction(accountId, amount, TransactionType.Deposit, account.Balance);
            _dbContext.SaveChanges();
        }


        public void CreateTransanction(int accountId, decimal amount, TransactionType transactionType, decimal balance)
        {
            var transaction = new Transaction()
            {
                AccountId = accountId,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Amount = amount,
                Balance = balance,
                Type = "Debit",
                Operation = transactionType.ToString(),
            };
            _dbContext.Transactions.Add(transaction);
        }

    }
}
