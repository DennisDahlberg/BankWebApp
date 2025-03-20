using DataAccessLayer.DTOs;
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
    }
}
