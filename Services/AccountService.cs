using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http.Features;
using DataAccessLayer.Enums;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using BankWebApp.Infrastructure.Paging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.Data.SqlClient;
using Mapster;

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
            return accounts.Adapt<List<AccountDTO>>();
        }

        public List<AccountWithCustomerNameDTO> GetAllAccounsFromCustomerExcludingOne(int accountId, int customerId)
        {
            var customer = _dbContext.Customers
                .Where(c => c.CustomerId == customerId)
                .FirstOrDefault();

            var accounts = _dbContext.Accounts
                .Where(a => a.AccountId != accountId &&
                a.Dispositions.Any(d => d.CustomerId == customerId));


            return accounts.Select(a => new AccountWithCustomerNameDTO
            {
                Balance = a.Balance,
                AccountId = a.AccountId,
                Givenname = customer.Givenname,
                Surname = customer.Surname,                
            }).ToList();
        }

        public PagedResult<AccountWithCustomerNameDTO> GetAllAccounsFromAllCustomerExcludingOne
            (int customerId, int page, string sortOrder, string sortBy, string q)
        {
            var accounts = _dbContext.Accounts
                .Where(a => a.Dispositions.Any(d => d.CustomerId != customerId))
                .Select(a => new
                {
                    AccountId = a.AccountId,
                    Balance = a.Balance,
                    Firstname = a.Dispositions
                        .Select(d => d.Customer.Givenname)
                        .FirstOrDefault(),
                    Lastname = a.Dispositions
                        .Select(d => d.Customer.Surname)
                        .FirstOrDefault()
                });

            if (!string.IsNullOrEmpty(q))
            {
                accounts = accounts
                    .Where(c => c.Firstname.Contains(q) ||
                    c.Lastname.Contains(q) ||
                    c.AccountId.ToString().Contains(q));
            }

            if (sortBy == "Id")
                accounts = sortOrder == "asc" ? accounts.OrderBy(c => c.AccountId) : accounts.OrderByDescending(c => c.AccountId);

            else if (sortBy == "Name")
                accounts = sortOrder == "asc" ? accounts.OrderBy(c => c.Lastname) : accounts.OrderByDescending(c => c.Lastname);

            else if (sortBy == "Balance")
                accounts = sortOrder == "asc" ? accounts.OrderBy(c => c.Balance) : accounts.OrderByDescending(c => c.Balance);

            var accountDTOs = accounts.Select(a => new AccountWithCustomerNameDTO()
            {
                AccountId = a.AccountId,
                Balance = a.Balance,
                Givenname = a.Firstname,
                Surname = a.Lastname,
            });

            return accountDTOs.GetPaged(page, 50);
        }

        public AccountDTO GetAccount(int accountId)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.AccountId == accountId);
            return account.Adapt<AccountDTO>();
        }

        public void Deposit(int accountId, decimal amount)
        {
            var account = _dbContext.Accounts.First(a => a.AccountId == accountId);
            account.Balance += amount;

            CreateTransanction(accountId, amount, TransactionType.Deposit, account.Balance);
            _dbContext.SaveChanges();
        }

        public ResultCode Withdrawal(int accountId, decimal amount)
        {
            var account = _dbContext.Accounts.First(a => a.AccountId == accountId);
            if (account.Balance < amount)
                return ResultCode.BalanceToLow;

            account.Balance -= amount;
            CreateTransanction(accountId, -amount, TransactionType.Withdrawal, account.Balance);
            _dbContext.SaveChanges();
            return ResultCode.Success;
        }

        public ResultCode Transfer(int accountFrom, int accountTo, decimal amount)
        {
            var accountTransferFrom = _dbContext.Accounts.First(a => a.AccountId == accountFrom);
            var accountTransferTo = _dbContext.Accounts.First(a => a.AccountId == accountTo);

            if (accountTransferFrom.Balance < amount)
                return ResultCode.BalanceToLow;

            accountTransferFrom.Balance -= amount;
            accountTransferTo.Balance += amount;

            CreateTransanction(accountFrom, -amount, TransactionType.TransferFromAccount, accountTransferFrom.Balance);
            CreateTransanction(accountTo, amount, TransactionType.TransferToAccount, accountTransferTo.Balance);

            _dbContext.SaveChanges();
            return ResultCode.Success;
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

        
        public void CreateAccount(int customerId)
        {
            var customer = _dbContext.Customers.First(c => c.CustomerId == customerId);

            var account = new Account()
            {
                Frequency = "Monthly",
                Created = DateOnly.FromDateTime(DateTime.Now),
                Balance = 0,
            };

            var disposition = new Disposition()
            {
                Account = account,
                Customer = customer,
                Type = "Owner"
            };

            _dbContext.Accounts.Add(account);
            _dbContext.Dispositions.Add(disposition);
            _dbContext.SaveChanges();
        }

    }
}
