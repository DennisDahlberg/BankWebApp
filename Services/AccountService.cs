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
                Firstname = customer.Givenname,
                Lastname = customer.Surname,                
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
                    Customers = a.Dispositions
                        .Select(d => d.Customer)
                        .Distinct()
                })
                .Select(a => new
                {
                    AccountId = a.AccountId,
                    Balance = a.Balance,
                    Firstname = a.Customers.First().Givenname,
                    Lastname = a.Customers.First().Surname
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

            else if (sortBy == "Name")
                accounts = sortOrder == "asc" ? accounts.OrderBy(c => c.Balance) : accounts.OrderByDescending(c => c.Balance);

            var accountDTOs = accounts.Select(a => new AccountWithCustomerNameDTO()
            {
                AccountId = a.AccountId,
                Balance = a.Balance,
                Firstname = a.Firstname,
                Lastname = a.Lastname,
            });

            return accountDTOs.GetPaged(page, 50);
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
