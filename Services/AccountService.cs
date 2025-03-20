using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
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





    }
}
