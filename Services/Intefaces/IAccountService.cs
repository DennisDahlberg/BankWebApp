using DataAccessLayer.DTOs;
using DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Intefaces
{
    public interface IAccountService
    {
        List<AccountDTO> GetAllAccountsFromCustomer(int customerId);
        AccountDTO GetAccount(int customerId);
        void CreateAccount(int customerId);
        void Deposit(int accountId, decimal amount);
        ResultCode Withdrawal(int accountId, decimal amount);
    }
}
