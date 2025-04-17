using BankWebApp.Infrastructure.Paging;
using DataAccessLayer.DTOs;
using DataAccessLayer.Enums;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAccountService
    {
        List<AccountDTO> GetAllAccountsFromCustomer(int customerId);
        List<AccountWithCustomerNameDTO> GetAllAccounsFromCustomerExcludingOne(int accountId, int customerId);
        PagedResult<AccountWithCustomerNameDTO> GetAllAccounsFromAllCustomerExcludingOne(int customerId, int page, string orderBy, string sortBy, string q);
        Result<AccountDTO> GetAccount(int accountId);
        void CreateAccount(int customerId);
        Task Delete(int accountId);
        void Deposit(int accountId, decimal amount);
        ResultCode Withdrawal(int accountId, decimal amount);
        ResultCode Transfer(int accountFrom, int accountTo, decimal amount);
        Task<bool> IsValid(int accountId, int customerId);
        decimal GetTotalBalance(int customerId);
    }
}
