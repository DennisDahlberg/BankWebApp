using DataAccessLayer.DTOs;
using Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService : IAccountService
    {
        public List<AccountDTO> GetAllAccountsFromCustomer(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
